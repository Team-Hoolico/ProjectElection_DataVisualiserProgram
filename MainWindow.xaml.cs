using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;

namespace ProjectElection_DataVisualiserProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HttpClient http = new HttpClient();
        public double[] TeamVotes = new double[2];
        public double TeamTotalVotes = 0;
        public int[] TeamIds = new int[2] {11,12};
        public TextBlock[] TeamText;
        public MainWindow()
        {
            http.BaseAddress = new Uri("https://localhost:7241/");
            InitializeComponent();
            TeamText = new TextBlock[] { TeamAVote, TeamBVote };
            GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e){
            GetData();
        }

        private async void GetData(){
            for(int i=0;i<TeamIds.Length;i++){
                TeamVotes[i] = Convert.ToInt32(await http.GetAsync($"/GetTeamVotes/?TeamId={TeamIds[i]}").Result.Content.ReadAsStringAsync());
                TeamText[i].Text = ""+TeamVotes[i];
            }
            TeamTotalVotes = TeamVotes.Sum();
            TeamTotalVote.Text = TeamTotalVotes + "";
            // Blue Colour
            PieChart.Slice = TeamTotalVotes>0?(TeamVotes[1]/TeamTotalVotes):0.5;
        }
    }
}
