using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        private byte[] errorFlag = new byte[4];

        public CreatePrizeForm( )
        {
            InitializeComponent( );
        }

        private void createPrizeButton_Click( object sender , EventArgs e )
        {
            if ( ValidateForm( ) )
            {
                PrizeModel model = new PrizeModel
                    (placeName:         placeNameValue.Text,
                     placeNumber:       placeNumberValue.Text,
                     prizeAmount:       prizeAmountValue.Text,
                     prizePercentage:   prizePercentageValue.Text
                    ) ;

                GlobalConfig.Connection.CreatePrize( model );

                placeNameValue.Text = "";
                placeNumberValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";
            }
            else
            {
                MessageBox.Show( "This form has invalid information, please check and try again" );
                ErrorMessage( errorFlag );
            }
        }

        private bool ValidateForm( )
        {
            bool output = true;
            int placeNumber = 0;
            if ( !int.TryParse( placeNumberValue.Text , out placeNumber ) )
            {
                errorFlag[ 0 ] = 1;
                output = false;
            }
            if ( placeNumber < 1 )
            {
                errorFlag[ 0 ] = 1;
                output = false;
            }

            if ( placeNameValue.Text.Length == 0 )
            {
                errorFlag[ 1 ] = 1;
                output = false;
            }

            decimal prizeAmount = 0;
            double prizePercentage = 0;
            if ( !decimal.TryParse( prizeAmountValue.Text , out prizeAmount ) )
            {
                errorFlag[ 2 ] = 1;

                output = false;
            }
            if ( !double.TryParse( prizePercentageValue.Text , out prizePercentage ) )
            {
                errorFlag[ 2 ] = 1;

                output = false;
            }

            if ( prizeAmount <= 0 && prizePercentage <= 0 )
            {
                errorFlag[ 2 ] = 1;
                errorFlag[ 3 ] = 1;
                output = false;
            }
            if ( prizePercentage < 0 || prizePercentage > 100 )
            {
                errorFlag[ 3 ] = 1;
                output = false;
            }
            return output;
        }

        public void ErrorMessage( byte[ ] errors )
        {
            string errorMessage = "";
            if ( errors[ 0 ] == 1 )
            {
                errorMessage += "Error at Place Number\n";
            }
            if ( errors[ 1 ] == 1 )
            {
                errorMessage += "Error at Place Name\n";
            }
            if ( errors[ 2 ] == 1 )
            {
                errorMessage += "Error at Prize Amount\n";
            }
            if ( errors[ 3 ] == 1 )
            {
                errorMessage += "Error at Prize Percentage\n";
            }
            MessageBox.Show( errorMessage );
        }
    }
}