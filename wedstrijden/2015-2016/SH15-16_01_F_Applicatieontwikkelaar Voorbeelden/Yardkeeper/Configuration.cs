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
        #region Class vars, Getters/Setters.

        private XmlDataDocument source;
        private XmlNodeList nodes;

        private const string SENSOR_DATA_CONFIG_FILE_NAME = @"Data\sensors.xml";
        private const string SCHEDULE_DATA_CONFIG_FILE_NAME = @"Data\schedule.xml";

        private string filename;
        public string FileName
        {
            get 
            {
                return filename;
            }
            set
            {
                if ( value != filename )
                {
                    if ( System.IO.File.Exists( value ) )
                    {
                        filename = value;
                        return;
                    }

                    throw new FileNotFoundException( "Could not locate the requested file!" );
                }
            }
        }

        public enum File 
        {
            WITH_SENSOR_DATA,
            WITH_SCHEDULE_DATA
        }

        #endregion

        #region IO File handling

        public XmlDataDocument GetFile()
        {
            return source;
        }

        /// <summary>
        ///     Set the configuration file for reading.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SetFile( File type )
        {
            FileStream document;

            switch ( type )
            {
                case File.WITH_SENSOR_DATA:
                    
                    FileName = SENSOR_DATA_CONFIG_FILE_NAME;
                break;

                case File.WITH_SCHEDULE_DATA:
                    
                    FileName = SCHEDULE_DATA_CONFIG_FILE_NAME;
                break;

                default:
                    
                    return false;
            }

            document = new FileStream( filename, FileMode.Open, FileAccess.Read );
            
            // XmlDataDocument is scheduled for retirement per the next major iteration of .NET.
            // A candidate may decide to replace it with xDocument, its successor.
            source = new XmlDataDocument();
            source.Load( document );
            
            // This is a very important part of this class.
            // By immediately releasing the lock on the file, 
            // multiple instances may use the it at the same time.
            // the source class var is loaded with the bitstream of the document and stored in memory.
            document.Dispose();
            
            if ( source == null )
            {
                throw new FileNotFoundException( "Could not read the requested file!" );
            }

            return true;
        }

        public void Save( int index, string xpath, object value )
        {
            XmlNodeList nodes = source.SelectNodes( xpath );

            if ( index < nodes.Count)
            {
                nodes[ index ].InnerXml = value.ToString();
            }
            
            source.Save( FileName );
        }

        #endregion

        #region Xml Document manipulation.

        /// <summary>
        ///     Method to collect a specific node within an Xml document.
        ///     Used to quickly navigate Xml documents.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public XmlNodeList GetValuesByNodeName( string node )
        {
            if ( nodes == null )
            {
                ScanForNode( node );
            }

            return nodes;
        }

        /// <summary>
        ///     Method to quickly determine whether the requested node exists within the loaded document.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool NodeExists( string node )
        {   
            if ( ScanForNode( node ) )
            {
                return true;
            }

            return false;            
        }

        /// <summary>
        ///     Worker method for NodeExists.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool ScanForNode( string node )
        {
            nodes = null;

            if ( source != null )
            {
                nodes = source.GetElementsByTagName( node );
 
                if ( nodes.Count > 0 )
                {
                    return true;
                }
            }
            
            return false;
        }

        #endregion
    }
}
