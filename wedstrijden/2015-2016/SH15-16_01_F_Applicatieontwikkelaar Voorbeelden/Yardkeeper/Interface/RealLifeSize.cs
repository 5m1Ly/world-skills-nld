using System;

namespace Yardkeeper
{
    public class RealLifeSize
    {
        public enum DistanceUnit
        { 
            Metric,
            Imperial
        }

        private DistanceUnit unit = DistanceUnit.Metric;
        public DistanceUnit Unit
        {
            get
            {
                return unit;
            }
            set
            {
                if ( value != unit )
                {
                    unit = value;
                }
            }
        }

        // 1 yard equals 91.44 cm, or 0.9144 meters.
        const float YARD_TO_METER_CONVERSION_FACTOR = .9144F;

        public RealLifeSize( double width, double length, DistanceUnit unit = DistanceUnit.Metric )
        {
            Width = width;
            Length = length;
        }

        public double Width { get; set; }
        public double Length { get; set; }

        // Provide overloads for the equality operators to enforce unity in the getters/setters.
        public static bool operator ==( RealLifeSize a, RealLifeSize b )
        {
            return ( a.Width == b.Width && a.Length == b.Length );
        }

        public static bool operator !=( RealLifeSize a, RealLifeSize b )
        {
            return !( a.Width == b.Width && a.Length == b.Length ); 
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void ToYards()
        {
            if ( unit != DistanceUnit.Imperial )
            { 
                Width /= YARD_TO_METER_CONVERSION_FACTOR;
                Length /= YARD_TO_METER_CONVERSION_FACTOR;
            }
        }

        public void ToMeters()
        {
            if ( unit != DistanceUnit.Metric )
            {
                Width *= YARD_TO_METER_CONVERSION_FACTOR;
                Length *= YARD_TO_METER_CONVERSION_FACTOR;
            }
        }
    }
}
