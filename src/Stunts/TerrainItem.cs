namespace Stunts
{
    public class TerrainItem : BaseItem
    {
        public TerrainItem(string id)
            : base(id)
        {
        }

        public static readonly TerrainItem Bottom = new TerrainItem("TB");
        public static readonly TerrainItem BottomSouthWest = new TerrainItem(Bottom + Separator + South + West);
        public static readonly TerrainItem BottomSouthEast = new TerrainItem(Bottom + Separator + South + East);
        public static readonly TerrainItem BottomNorthWest = new TerrainItem(Bottom + Separator + North + West);
        public static readonly TerrainItem BottomNorthEast = new TerrainItem(Bottom + Separator + North + East);

        public static readonly TerrainItem Top = new TerrainItem("TT");
        public static readonly TerrainItem TopSouthWest = new TerrainItem(Top + Separator + South + West);
        public static readonly TerrainItem TopSouthEast = new TerrainItem(Top + Separator + South + East);
        public static readonly TerrainItem TopNorthWest = new TerrainItem(Top + Separator + North + West);
        public static readonly TerrainItem TopNorthEast = new TerrainItem(Top + Separator + North + East);

        public static readonly TerrainItem Side = new TerrainItem("TS");

        public static implicit operator TerrainItem(string item) => FindItem<TerrainItem>(item);
    }
}