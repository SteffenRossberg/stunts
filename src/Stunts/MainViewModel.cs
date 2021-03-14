using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;

namespace Stunts
{
    class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            RefreshTrackList();
        }

        private BaseItem _currentItem;
        public BaseItem CurrentItem
        {
            get => _currentItem;
            set => Set(ref _currentItem, value);
        }

        private string _selectedTrackFile;
        public string SelectedTrackFile
        {
            get => _selectedTrackFile;
            set
            {
                if (Set(ref _selectedTrackFile, value) && _selectedTrackFile != null)
                {
                    Load(_selectedTrackFile);
                    TrackFileName = _selectedTrackFile;
                }

                if (Workspace == null || Track == null)
                {
                    Reset();
                }

                if (value == null)
                {
                    RaisePropertyChanges();
                }
            }
        }

        private string _trackFileName;
        public string TrackFileName
        {
            get => _trackFileName;
            set => Set(ref _trackFileName, value);
        }

        private ObservableCollection<string> _trackFiles;
        public ObservableCollection<string> TrackFiles
        {
            get => _trackFiles;
            set => Set(ref _trackFiles, value);
        }

        private ICommand _selectItemCommand;
        public ICommand SelectItemCommand => _selectItemCommand ??= new DelegateCommand(OnSelectItem);
        private void OnSelectItem(object item)
            => CurrentItem = (BaseItem) item;

        private ICommand _applyItemCommand;
        public ICommand ApplyItemCommand => _applyItemCommand ??= new DelegateCommand(OnApplyItem);
        private void OnApplyItem(object parameter)
        {
            if (parameter is int[] position)
            {
                var row = position[0];
                var column = position[1];
                Apply((dynamic)CurrentItem, row, column);
            }
        }
        private void Apply(TerrainItem terrainItem, int row, int column) => Workspace[row][column] = terrainItem;
        private void Apply(StreetItem streetItem, int row, int column)
        {
            if (streetItem == StreetItem.StartFinishNorth || streetItem == StreetItem.StartFinishSouth || streetItem == StreetItem.StartFinishWest || streetItem == StreetItem.StartFinishEast)
            {
                Track.ForEach((i, r) => r.Assign((j, item) =>
                {
                    if (item == StreetItem.StartFinishNorth || item == StreetItem.StartFinishSouth)
                        return StreetItem.StreetNorthSouth;
                    if (item == StreetItem.StartFinishWest || item == StreetItem.StartFinishEast)
                        return StreetItem.StreetEastWest;
                    return item;
                }));
            }
            Track[row][column] = streetItem;
        }

        private ICommand _newCommand;
        public ICommand NewCommand => _newCommand ??= new DelegateCommand(OnNew);
        private void OnNew(object parameter)
        {
            Reset();
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(OnSave);
        private void OnSave(object parameter)
        {
            var directory = EnsureDirectory("Tracks");
            var trackFileName = TrackFileName;
            var file = Path.Combine(directory, $"{TrackFileName}.json");
            var workspace = Serialize(Workspace);
            var track = Serialize(Track);
            var content = $"{{\n  \"workspace\": {workspace},\n  \"track\": {track}\n}}";
            File.WriteAllText(file, content);
            RefreshTrackList();
            SelectedTrackFile = trackFileName;
        }

        private string EnsureDirectory(string name)
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, name);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }

        private void Load(string trackFileName)
        {
            var directory = EnsureDirectory("Tracks");
            var file = Path.Combine(directory, $"{trackFileName}.json");
            var content = File.ReadAllText(file);
            var trackFile = JsonConvert.DeserializeObject<TrackFile>(content);
            Workspace = trackFile.Workspace;
            Track = trackFile.Track;
        }

        private void RefreshTrackList()
        {
            var directory = EnsureDirectory("Tracks");
            TrackFiles = new ObservableCollection<string>(Directory.GetFiles(directory).Select(file => Path.GetFileNameWithoutExtension(file)));
            SelectedTrackFile = TrackFiles.FirstOrDefault();
        }

        private string Serialize<TItem>(ObservableCollection<ObservableCollection<TItem>> data) where TItem : BaseItem
            => $"[\n    {string.Join(",\n    ", data.Select(row => $"[\"{string.Join("\", \"", row)}\"]"))}\n  ]";

        private ObservableCollection<ObservableCollection<TerrainItem>> _workspace;
        public ObservableCollection<ObservableCollection<TerrainItem>> Workspace
        {
            get => _workspace;
            private set => Set(ref _workspace, value);
        }

        private ObservableCollection<ObservableCollection<StreetItem>> _track;
        public ObservableCollection<ObservableCollection<StreetItem>> Track
        {
            get => _track;
            private set => Set(ref _track, value);
        }

        private static ObservableCollection<ObservableCollection<TItem>> CreateWorkspace<TItem>(TItem defaultValue) where TItem : BaseItem
            => new ObservableCollection<ObservableCollection<TItem>>(
                Enumerable
                    .Range(0, Settings.RowCount)
                    .Select(row =>
                        new ObservableCollection<TItem>(Enumerable.Range(0, Settings.ColumnCount).Select(column => defaultValue))));

        private void Reset()
        {
            Workspace = CreateWorkspace(TerrainItem.Bottom);
            Track = CreateWorkspace(StreetItem.Empty);
        }
    }

    public class TrackFile
    {
        public ObservableCollection<ObservableCollection<TerrainItem>> Workspace { get; set; }
        public ObservableCollection<ObservableCollection<StreetItem>> Track { get; set; }
    }
}
