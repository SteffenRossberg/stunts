namespace Stunts
{
    public class StreetItem : BaseItem
    {
        public StreetItem(string id)
            : base(id)
        {
        }

        public static readonly StreetItem Empty = new StreetItem("NULL");

        private static readonly StreetItem StartFinish = new StreetItem("SF");
        public static readonly StreetItem StartFinishNorth = new StreetItem(StartFinish + Separator + North);
        public static readonly StreetItem StartFinishSouth = new StreetItem(StartFinish + Separator + South);
        public static readonly StreetItem StartFinishWest = new StreetItem(StartFinish + Separator + West);
        public static readonly StreetItem StartFinishEast = new StreetItem(StartFinish + Separator + East);

        private static readonly StreetItem Street = new StreetItem("ST");
        public static readonly StreetItem StreetEastWest = new StreetItem(Street + Separator + East + West);
        public static readonly StreetItem StreetNorthSouth = new StreetItem(Street + Separator + North + South);

        public static readonly StreetItem SmallCross = new StreetItem("SCR");
        public static readonly StreetItem SmallCrossWestSouthEast = new StreetItem(SmallCross + Separator + West + South + East);
        public static readonly StreetItem SmallCrossWestNorthEast = new StreetItem(SmallCross + Separator + West + North + East);
        public static readonly StreetItem SmallCrossEastSouthWest = new StreetItem(SmallCross + Separator + East + South + West);
        public static readonly StreetItem SmallCrossEastNorthWest = new StreetItem(SmallCross + Separator + East + North + West);
        public static readonly StreetItem SmallCrossSouthWestNorth = new StreetItem(SmallCross + Separator + South + West + North);
        public static readonly StreetItem SmallCrossSouthEastNorth = new StreetItem(SmallCross + Separator + South + East + North);
        public static readonly StreetItem SmallCrossNorthWestSouth = new StreetItem(SmallCross + Separator + North + West + South);
        public static readonly StreetItem SmallCrossNorthEastSouth = new StreetItem(SmallCross + Separator + North + East + South);

        private static readonly StreetItem SmallCurve = new StreetItem("SCU");
        public static readonly StreetItem SmallCurveNorthEast = new StreetItem(SmallCurve + Separator + North + East);
        public static readonly StreetItem SmallCurveNorthWest = new StreetItem(SmallCurve + Separator + North + West);
        public static readonly StreetItem SmallCurveSouthEast = new StreetItem(SmallCurve + Separator + South + East);
        public static readonly StreetItem SmallCurveSouthWest = new StreetItem(SmallCurve + Separator + South + West);

        public static implicit operator StreetItem(string item) => FindItem<StreetItem>(item);
    }
}