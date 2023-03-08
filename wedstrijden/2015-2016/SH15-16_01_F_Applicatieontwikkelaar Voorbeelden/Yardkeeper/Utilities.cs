using System;
using System.Collections.Generic;

using System.Text;

using System.Threading.Tasks;

namespace Yardkeeper
{
    public class Utilities
    {
        public static double GetNumericValueFromText( string text )
        {
            if ( text == String.Empty )
            {
                return 0D;
            }

            
            double number;
            int modifier = 1;

            if ( double.TryParse( text, out number ) )
            {
                if ( text.StartsWith( "-" ) )
                {
                    modifier = -1;
                }   
 
                return number *= modifier;
            }
                    
            throw new ArgumentException( "Cannot parse number from text value!" );
        }
    }
}
