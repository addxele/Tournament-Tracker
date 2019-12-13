using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelper
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath( this string fileName )
        {
            // save to {file from app.config}\fileName
            return $"{ ConfigurationManager.AppSettings[ "filePath" ] }\\{ fileName }";
        }

        /// <summary>
        /// Load Text File
        /// </summary>
        /// <param name="file"> </param>
        /// <returns> </returns>
        public static List<string> LoadFile( this string file )
        {
            if ( !File.Exists( file ) )
            {
                return new List<string>( );
            }
            return new List<string>( File.ReadAllLines( file ) );
        }

        public static List<PersonModel> ConvertToPersonModel( this List<string> lines )
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach ( string line in lines )
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel
                {
                    Id = int.Parse(cols[0].Trim()),
                    FirstName = cols[1].Trim(),
                    LastName = cols[ 2 ].Trim(),
                    EmailAddress = cols[3].Trim(),
                    CellphoneNumber = cols[4].Trim()
                };

                output.Add( p );
            }
            return output;
        }

        public static List<PrizeModel> ConvertToPrizeModel( this List<string> lines )
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach ( string line in lines )
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel
                {
                    Id = int.Parse( cols[ 0 ] .Trim()),
                    PlaceNumber = int.Parse( cols[ 1 ].Trim() ),
                    PlaceName = cols[ 2 ].Trim(),
                    PrizeAmount = decimal.Parse( cols[ 3 ].Trim() ),
                    PrizePercentage = double.Parse( cols[ 4 ].Trim() )
                };
                output.Add( p );
            }

            return output;
        }

        public static List<TeamModel> ConvertToTeamModel( this List<string> lines , string peopleFileName )
        {
            List<TeamModel> output =new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModel();

            foreach ( string line in lines )
            {
                string[] cols = line.Split(',');
                TeamModel t = new TeamModel
                {
                    Id = int.Parse(cols[0].Trim()),
                    TeamName = cols[1].Trim()
                };

                string[] personId = cols[2].Split('|');

                foreach ( string id in personId )
                {
                    t.TeamMembers.Add( ( PersonModel ) people.Where( x => x.Id == int.Parse( id ) ).First( ) );
                }
            }

            return output;
        }

        public static List<T> ConvertToModel<T>( this List<string> lines )
            where T : Model
        {
            List<T> outp0ut = new List<T>();

            Model model;

            if ( typeof( T ) == typeof( PersonModel ) )
            {
                model = new PersonModel( );
            }
            else if ( typeof( T ) == typeof( PrizeModel ) )
            {
                model = new PrizeModel( );
            }
            else if ( typeof( T ) == typeof( TeamModel ) )
            {
                model = new TeamModel( );
            }
            else if ( typeof( T ) == typeof( TournamentModel ) )
            {
                model = new TournamentModel( );
            }
            else if ( typeof( T ) == typeof( MatchupEntryModel ) )
            {
                model = new MatchupEntryModel( );
            }

            foreach ( string line in lines )
            {
                string[] cols = line.Split(',');
            }

            return new List<T>( );
        }

        public static void SaveToPrizeFile( this List<PrizeModel> model , string fileName )
        {
            List<string> lines = new List<string>();

            foreach ( PrizeModel p in model )
            {
                lines.Add( $"{p.Id}, {p.PlaceNumber}, {p.PlaceName}, {p.PrizeAmount}, {p.PrizePercentage}" );
            }

            File.WriteAllLines( fileName.FullFilePath( ) , lines );
        }

        public static void SaveToPersonFile( this List<PersonModel> model , string fileName )
        {
            List<string> lines = new List<string>();
            foreach ( PersonModel p in model )
                lines.Add( $"{p.Id}, {p.FirstName}, {p.LastName}, {p.EmailAddress}, {p.CellphoneNumber}" );
            File.WriteAllLines( fileName.FullFilePath( ) , lines );
        }

        public static void SaveToTeamFile( this List<TeamModel> model , string fileName )
        {
            List<string> lines = new List<string>();
            foreach ( TeamModel t in model )
            {
                lines.Add( $"{ t.Id}, { t.TeamName }, { ConvertPeopleListToString( t.TeamMembers )}" );
            }

            File.WriteAllLines( fileName.FullFilePath( ) , lines );
        }

        private static string ConvertPeopleListToString( List<PersonModel> people )
        {
            string output = "";

            if ( people.Count <= 0 )
                return output;

            foreach ( PersonModel p in people )
            {
                output += $"{p.Id}|";
            }
            return output.Substring( 0 , output.Length - 1 );
        }
    }
}