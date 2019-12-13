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

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private byte[ ] errorFlags = new byte[ 4 ];

        private List<PersonModel> availableTeamMembers= GlobalConfig.Connection.GetPerson_All( );
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm( )
        {
            InitializeComponent( );

            //CreateSampleData( );

            WireUpLists( );
        }

        private void CreateSampleData( )
        {
            availableTeamMembers.Add( new PersonModel
            {
                FirstName = "Tim" ,
                LastName = "Corey"
            } );
            availableTeamMembers.Add( new PersonModel
            {
                FirstName = "Sue" ,
                LastName = "Storm"
            } );
            availableTeamMembers.Add( new PersonModel
            {
                FirstName = "Mister" ,
                LastName = "Incredible"
            } );

            selectedTeamMembers.Add( new PersonModel
            {
                FirstName = "John" ,
                LastName = "Marstin"
            } );
            selectedTeamMembers.Add( new PersonModel
            {
                FirstName = "Peter" ,
                LastName = "Parker"
            } );
            selectedTeamMembers.Add( new PersonModel
            {
                FirstName = "Carol" ,
                LastName = "Danver"
            } );
        }

        private void WireUpLists( )
        {
            //TODO: May need to find better solution for refreshing
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMemberListBox.DataSource = null;
            teamMemberListBox.DataSource = selectedTeamMembers;
            teamMemberListBox.DisplayMember = "FullName";
        }

        private void createMemberButton_Click( object sender , EventArgs e )
        {
            if ( ValidateForm( ) )
            {
                PersonModel model = new PersonModel
                {
                    FirstName  = firstNameValue.Text,
                    LastName = lastNameValue.Text,
                    EmailAddress = emailValue.Text,
                    CellphoneNumber = cellphoneValue.Text
                };

                model = GlobalConfig.Connection.CreatePerson( model );

                selectedTeamMembers.Add( model );

                WireUpLists( );

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show( "This form has invalid information, please check and try again" );
                ErrorMessage( errorFlags );
            }
        }

        private bool ValidateForm( )
        {
            bool output = true;

            if ( firstNameValue.Text == "" )
                errorFlags[ 0 ] = 1;

            if ( lastNameValue.Text == "" )
                errorFlags[ 1 ] = 1;
            if ( emailValue.Text == "" )
                errorFlags[ 2 ] = 1;
            if ( cellphoneValue.Text.Length == 0 )
                errorFlags[ 3 ] = 1;
            if ( errorFlags[ 0 ] == 1 || errorFlags[ 1 ] == 1 || errorFlags[ 2 ] == 1 || errorFlags[ 3 ] == 1 )
                output = false;

            return output;
        }

        private void ErrorMessage( byte[ ] errors )
        {
            string errorMessage = "";
            if ( errors[ 0 ] == 1 )
            {
                errorMessage += "Error at First Name\n";
            }
            if ( errors[ 1 ] == 1 )
            {
                errorMessage += "Error at Last Name\n";
            }
            if ( errors[ 2 ] == 1 )
            {
                errorMessage += "Error at Email\n";
            }
            if ( errors[ 3 ] == 1 )
            {
                errorMessage += "Error at Cellphone\n";
            }
            MessageBox.Show( errorMessage );
        }

        private void addTeamMemberButton_Click( object sender , EventArgs e )
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if ( p != null )
            {
                availableTeamMembers.Remove( p );
                selectedTeamMembers.Add( p );

                WireUpLists( );
            }
        }

        private void removeSelectedMemberButton_Click( object sender , EventArgs e )
        {
            PersonModel p = (PersonModel)teamMemberListBox.SelectedItem;
            if ( p != null )
            {
                selectedTeamMembers.Remove( p );
                availableTeamMembers.Add( p );

                WireUpLists( );
            }
        }
    }
}