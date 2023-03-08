using System;
using System.Collections.Generic;

using System.IO;
using System.Xml;
using System.Text;

using System.Threading.Tasks;

namespace Yardkeeper
{
    class Configuration
    {
        private XmlDataDocument source;

        // Todo: figure out the best way to load the files... 

        ///// <summary>
        /////     Set the configuration file for reading.
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public bool SetFile( File type )
        //{
        //    FileStream document;
        //    source.Load( document );
            
        //    document.Dispose();
            
        //    if ( source == null )
        //    {
        //        throw new FileNotFoundException( "Could not read the requested file!" );
        //    }

        //    return true;
        //}

        //public XmlNodeList GetValuesByNodeName( string node )
        //{
        //    if ( nodes == null )
        //    {
        //        ScanForNode( node );
        //    }

        //    return nodes;
        //}

        //public bool NodeExists( string node )
        //{   
        //    if ( ScanForNode( node ) )
        //    {
        //        return true;
        //    }

        //    return false;            
        //}
    }
}
