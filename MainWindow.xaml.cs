using SMCA.Controllers;
using SMCA.Models;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SMCA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsTickerRunning { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            SetbtnTickerPlayStopContent();
        }
        private void MediaDropArea_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void MediaDropArea_Drop(object sender, DragEventArgs e)
        {
        }

        private void BrowseMedia_Click(object sender, RoutedEventArgs e)
        {
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbQuote.IsChecked.Value == true)
            {

                labelPostText.Content = "Post text:";
                tboxMainBody_XRIBT.IsEnabled = true;
                labelQuoteLink.IsEnabled = true;
                labelQuoteLink.Content = "Quoted post link";
                tboxQuoteLink_RXB.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnTickerPlayStop_Click(object sender, RoutedEventArgs e)
        {
            IsTickerRunning = !IsTickerRunning;
            SetbtnTickerPlayStopContent();
        }

        //minimethods

        /// <summary>
        /// sets what is on button for running ticker after click
        /// </summary>
        private void SetbtnTickerPlayStopContent()
        {
            if (IsTickerRunning)
            {
                btnTickerPlayStop.Content = "⭕";
            }
            else
            {
                btnTickerPlayStop.Content = "❌";
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl &&
        tabControl.SelectedItem is TabItem selectedTab &&
        selectedTab.Header?.ToString() == "About/Help")
            {
                webReadme.Navigate("https://github.com/Estlib/SMCA/blob/main/README.md");
            }
        }

        private void Btn_PM_SchedulePosts_Click(object sender, RoutedEventArgs e)
        {
            ConsoleController.Normal("ACTION: Btn_PM_SchedulePosts_Click()");
            ConsoleController.Info("Gathering data from form.");
            string missingfields = "";

            ContaineredPost container = new ContaineredPost();
            container.ID = Guid.NewGuid();
            container.IsDraft = false;
            container.CreatedAt = DateTime.Now;
            container.ModifiedAt = DateTime.Now;
            List<Platform> publishplatforms = new List<Platform>();

            if (tickXoption.IsChecked == true)
            {
                publishplatforms.Add(Platform.Twitter);
            }
            if (tickIoption.IsChecked == true)
            {
                publishplatforms.Add(Platform.Instagram);
            }
            if (tickRoption.IsChecked == true)
            {
                publishplatforms.Add(Platform.Reddit);
            }
            if (tickBoption.IsChecked == true)
            {
                publishplatforms.Add(Platform.BlueSky);
            }
            if (tickToption.IsChecked == true)
            {
                publishplatforms.Add(Platform.Threads);
            }

            if (publishplatforms.Count < 1)
            {
                ConsoleController.Error("No platforms selected, creation aborted.");
                MessageBox.Show("Please select atleast one platform.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                ConsoleController.Normal("Found platforms: " + publishplatforms.Count);
            }
            List<DateTime> publishtimes = GetPublishTimes(publishplatforms);
            if (publishtimes == null)
            {
                ConsoleController.Error("Publishing times missing, creation aborted.");
                MessageBox.Show("Please set the time for the post to be published on.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (publishtimes.Count != publishplatforms.Count)
            {
                ConsoleController.Error($"Publishing times and platforms are out of sync, creation aborted.\ntimes {publishtimes.Count} || platforms {publishplatforms.Count}");
                MessageBox.Show($"Publishing times and platforms are out of sync, creation aborted.\ntimes {publishtimes.Count} || platforms {publishplatforms.Count}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            container.PublishOnPlatforms = publishplatforms;
            container.PublishThisAt = publishtimes;
            ConsoleController.Normal($"Got platforms and times successfully:\n {string.Join(", ", publishplatforms)}\n {string.Join(", ", publishtimes)}");

            if (rbNewPost.IsChecked == false && rbRepost.IsChecked == false && rbQuote.IsChecked == false)
            {
                ConsoleController.Error("Missing: Post type, creation aborted.");
                MessageBox.Show($"Post type is not selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PostModel maindata = new PostModel();

            // Reddit-specific validation
            if (tickRoption.IsChecked == true)
            {
                bool isRedditOk = PartialValidator("R", out string failedparts);
                missingfields += failedparts;
                if (isRedditOk)
                {
                    // Reddit-only options, inaccessible properties are preset null here.
                    maindata.Title = tboxTitle_R.Text;                                  // R
                    maindata.Flair = null;                                              // R not supported yet
                    maindata.Spoiler = tickRspoiler.IsChecked.Value;                    // R
                    maindata.OriginalContent = tickRoriginalcontent.IsChecked.Value;    // R
                }
                else
                {
                    ConsoleController.Error("Missing reddit data after validation, creation aborted.");
                    MessageBox.Show($"Reddit post is missing some data.\n\n{failedparts}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            // bluesky-specific validation
            if (tickBoption.IsChecked == true)
            {
                bool isBlueSkyOk = PartialValidator("B", out string failedparts);
                missingfields += failedparts;
                //string qtlink = ExtractLink(tboxQuoteLink_RXB.Text);
                //if (qtlink != null) 
                //{
                //    container.
                //}
            }

            ConsoleController.Info("Btn_PM_SchedulePosts_Click() finished.");
        }
        /// <summary>
        /// can validate parts of form. To use, type singular letter into a string.
        /// R reddit, I instagram, X twitter, T threads, B bloesky
        /// </summary>
        /// <param name="v">Parts to validate</param>
        /// <returns>true or false</returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool PartialValidator(string parts, out string failedparts)
        {
            ConsoleController.Info($"PartialValidator() called on {parts}.");
            List<bool> isValid = new List<bool>();
            failedparts = "";

            // platform specific validation
            if (parts.Contains('R'))
            {
                if (string.IsNullOrWhiteSpace(tboxTitle_R.Text))
                {
                    failedparts += "Reddit title missing\n";
                    isValid.Add(false);
                }
                if (tboxTitle_R.Text.Length > 300)
                {
                    failedparts += "Reddit title too long, max count is 300\n";
                    isValid.Add(false);
                }
                else
                {
                    isValid.Add(true);
                }
            }
            if (parts.Contains('B'))
            {
                //unvalidible parameters:
                /*
                 * LinkTitle
                 * LinkDescription
                 * LinkThumbnail
                 * ThreadRoot
                 * Language
                 */

                //if (rbRepost.IsChecked == true && )
                //{
                //    failedparts += "Link to original post to repost missing\n";
                //    isValid.Add(false);
                //}
                //else
                //{
                //    isValid.Add(true);
                //}

                //currently does nothing, bluesky needs a bit more indepth validation
            }
            if (parts.Contains('X'))
            {
                //invalidible parameters
                /*
                 * ShareToFollowers
                 */
            }
            if (parts.Contains('I'))
            {
                //invalidible parameters
                /*
                 * Collaborators
                 */
            }

            // multiplaform parameter validation
            if (parts.Contains('R') && parts.Contains('X'))
            {
                //invalidible parameters
                /*
                 * PollOptions
                 * Community
                 */
            }
            if (parts.Contains('R') && parts.Contains('B'))
            {
                //invalidible parameters
                /*
                 * NSFW - is either true or false, no validation needed
                 */
            }
            if (parts.Contains('X') && parts.Contains('B'))
            {
                bool isThereALink = ContainsLink(tboxQuoteLink_RXB.Text);
                if (rbQuote.IsChecked.Value == true && isThereALink)
                {
                    string extractedLink = ExtractLink(tboxQuoteLink_RXB.Text);
                    Platform linkPlatform = new Platform();
                    if (extractedLink != null)
                    {
                        if (extractedLink.Contains("x.com") || extractedLink.Contains("twitter.com"))
                        {
                            linkPlatform = Platform.Twitter;
                            ConsoleController.Info($"Link is Twitter.");
                        }
                        else if (extractedLink.Contains("reddit.com")||extractedLink.Contains("redd.it"))
                        {
                            linkPlatform = Platform.Reddit;
                            ConsoleController.Info($"Link is Reddit.");
                        }
                        else if (extractedLink.Contains("bsky.app"))
                        {
                            linkPlatform = Platform.BlueSky;
                            ConsoleController.Info($"Link is BlueSky.");
                        }
                        else if (extractedLink.Contains("instagram.com"))
                        {
                            linkPlatform = Platform.Instagram;
                            ConsoleController.Info($"Link is Instagram.");
                        }
                        else if (extractedLink.Contains("threads.net"))
                        {
                            linkPlatform = Platform.Threads;
                            ConsoleController.Info($"Link is Threads.");
                        }
                        else
                        {
                            ConsoleController.Warning("Unknown platform specified, creation halted.");
                            var resultquestion = MessageBox.Show($"The link to a post youre Quoting is not from a platform\nhandled by this app, did you mean to make a \nnormal post?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                            if (resultquestion == MessageBoxResult.Yes)
                            {
                                ConsoleController.Highlight("User respecified post type, creation continued.");
                                rbNewPost.IsChecked = true;
                                rbRepost.IsChecked = false;
                                rbQuote.IsChecked = false;
                            }
                            else
                            {
                                failedparts += "Link detection error, platform unknown";
                                isValid.Add(false);
                            }
                        }
                    }
                    else 
                    {
                        failedparts += "Link detection error, please recheck link format";
                        ConsoleController.Error("Match is null, despite a link existing. Creation aborted.");
                        MessageBox.Show($"Something has gone wrong with link in your post, creation aborted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                        //bool doesLinkExistInDB = false;
                }
            }
            if (parts.Contains('X') && parts.Contains('B') && parts.Contains('R'))
            {
                bool isThereALink = ContainsLink(tboxQuoteLink_RXB.Text);
                if (rbRepost.IsChecked.Value == true && isThereALink)
                {
                    string extractedLink = ExtractLink(tboxQuoteLink_RXB.Text);
                    Platform linkPlatform = new Platform();
                    if (extractedLink != null)
                    {
                        if (extractedLink.Contains("x.com") || extractedLink.Contains("twitter.com"))
                        {
                            linkPlatform = Platform.Twitter;
                            ConsoleController.Info($"Link is Twitter.");
                        }
                        else if (extractedLink.Contains("reddit.com")||extractedLink.Contains("redd.it"))
                        {
                            linkPlatform = Platform.Reddit;
                            ConsoleController.Info($"Link is Reddit.");
                        }
                        else if (extractedLink.Contains("bsky.app"))
                        {
                            linkPlatform = Platform.BlueSky;
                            ConsoleController.Info($"Link is BlueSky.");
                        }
                        else if (extractedLink.Contains("instagram.com"))
                        {
                            linkPlatform = Platform.Instagram;
                            ConsoleController.Info($"Link is Instagram.");
                        }
                        else if (extractedLink.Contains("threads.net"))
                        {
                            linkPlatform = Platform.Threads;
                            ConsoleController.Info($"Link is Threads.");
                        }
                        else
                        {
                            ConsoleController.Warning("Unknown platform specified, creation halted.");
                            var resultquestion = MessageBox.Show($"The link to a post youre Quoting is not from a platform\nhandled by this app, did you mean to make a \nnormal post?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                            if (resultquestion == MessageBoxResult.Yes)
                            {
                                ConsoleController.Highlight("User respecified post type, creation continued.");
                                rbNewPost.IsChecked = true;
                                rbRepost.IsChecked = false;
                                rbQuote.IsChecked = false;
                            }
                            else
                            {
                                failedparts += "Link detection error, platform unknown";
                                isValid.Add(false);
                            }
                        }
                    }
                    else 
                    {
                        failedparts += "Link detection error, please recheck link format";
                        ConsoleController.Error("Match is null, despite a link existing. Creation aborted.");
                        MessageBox.Show($"Something has gone wrong with link in your post, creation aborted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                        //bool doesLinkExistInDB = false;
                }
            }

            

            // all-platform data

            // final validation verdict
            if (isValid.Contains(false))
            {
                ConsoleController.Error($"Validation failed.");
                return false;
            }
            else
            {
                ConsoleController.Highlight($"Validation passed.");
                return true;
            }
        }

        private string? ExtractLink(string text)
        {
            Match match = Regex.Match(text,@"https?://[^\s]+", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value.TrimEnd('.', ',', ';', '!', '?', ')');
            }
            else
            {
                return null;
            }
        }

        private bool ContainsLink(string text)
        {
            // clanker code
            if (string.IsNullOrWhiteSpace(text))
                return false;

            return Regex.IsMatch(
                text,
                @"((http|https):\/\/)?([\w-]+\.)+[a-z]{2,}([\/\w\-._~:/?#[\]@!$&'()*+,;=%]*)?",
                RegexOptions.IgnoreCase);
        }

        private List<DateTime> GetPublishTimes(List<Platform> publishplatforms)
        {
            List<DateTime> publishdates = new List<DateTime>();
            string errors = "";

            if (publishplatforms.Contains(Platform.Twitter))
            {
                if (dpickX.SelectedDate != null && tboxTimeX.Text != null)
                {
                    TimeSpan time = TimeParser(tboxTimeX.Text);
                    DateTime scheduledTime = dpickX.SelectedDate.Value.Add(time);
                    publishdates.Add(scheduledTime);
                }
                else if (dpickX.SelectedDate == null)
                {
                    errors += $"¤ Twitter publish date\n";
                    ConsoleController.Error("Missing: Twitter publish date.");
                }
                else if (tboxTimeX.Text == null || tboxTimeR.Text == string.Empty)
                {
                    errors += $"¤ Twitter publish time\n";
                    ConsoleController.Error("Missing: Twitter publish time.");
                }
            }

            if (publishplatforms.Contains(Platform.Reddit))
            {
                if (dpickR.SelectedDate != null && tboxTimeR.Text != null)
                {
                    TimeSpan time = TimeParser(tboxTimeR.Text);
                    DateTime scheduledTime = dpickR.SelectedDate.Value.Add(time);
                    publishdates.Add(scheduledTime);
                }
                else if (dpickR.SelectedDate == null)
                {
                    errors += $"¤ Reddit publish date\n";
                    ConsoleController.Error("Missing: Reddit publish date.");
                }
                else if (tboxTimeR.Text == null || tboxTimeR.Text == string.Empty)
                {
                    errors += $"¤ Reddit publish time\n";
                    ConsoleController.Error("Missing: Reddit publish time.");
                }
            }

            if (publishplatforms.Contains(Platform.Instagram))
            {
                if (dpickI.SelectedDate != null && tboxTimeI.Text != null)
                {
                    TimeSpan time = TimeParser(tboxTimeI.Text);
                    DateTime scheduledTime = dpickI.SelectedDate.Value.Add(time);
                    publishdates.Add(scheduledTime);
                }
                else if (dpickI.SelectedDate == null)
                {
                    errors += $"¤ Instagram publish date\n";
                    ConsoleController.Error("Missing: Instagram publish date.");
                }
                else if (tboxTimeI.Text == null || tboxTimeI.Text == string.Empty)
                {
                    errors += $"¤ Instagram publish time\n";
                    ConsoleController.Error("Missing: Instagram publish time.");
                }
            }

            if (publishplatforms.Contains(Platform.Threads))
            {
                if (dpickT.SelectedDate != null && tboxTimeT.Text != null)
                {
                    TimeSpan time = TimeParser(tboxTimeT.Text);
                    DateTime scheduledTime = dpickT.SelectedDate.Value.Add(time);
                    publishdates.Add(scheduledTime);
                }
                else if (dpickT.SelectedDate == null)
                {
                    errors += $"¤ Instagram publish date\n";
                    ConsoleController.Error("Missing: Instagram publish date.");
                }
                else if (tboxTimeT.Text == null || tboxTimeT.Text == string.Empty)
                {
                    errors += $"¤ Instagram publish time\n";
                    ConsoleController.Error("Missing: Instagram publish time.");
                }
            }

            if (publishplatforms.Contains(Platform.BlueSky))
            {
                if (dpickB.SelectedDate != null && tboxTimeB.Text != null)
                {
                    TimeSpan time = TimeParser(tboxTimeB.Text);
                    DateTime scheduledTime = dpickB.SelectedDate.Value.Add(time);
                    publishdates.Add(scheduledTime);
                }
                else if (dpickB.SelectedDate == null)
                {
                    errors += $"¤ BlueSky publish date\n";
                    ConsoleController.Error("Missing: BlueSky publish date.");
                }
                else if (tboxTimeB.Text == null || tboxTimeB.Text == string.Empty)
                {
                    errors += $"¤ BlueSky publish time\n";
                    ConsoleController.Error("Missing: BlueSky publish time.");
                }
            }
            if (publishdates.Count > 0)
            {
                return publishdates;
            }
            else
            {
                MessageBox.Show($"You are missing some scheduling data\n\n.{errors}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }

        private TimeSpan TimeParser(string text)
        {
            if (TimeSpan.TryParseExact(text, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan time))
            {
                return time;
            }

            return TimeSpan.Zero;
        }

        private void rbNewPost_Checked(object sender, RoutedEventArgs e)
        {
            if (rbNewPost.IsChecked.Value == true)
            {
                labelPostText.Content = "Post text:";
                tboxMainBody_XRIBT.IsEnabled = true;
                labelQuoteLink.IsEnabled = false;
                labelQuoteLink.Content = "---";
                tboxQuoteLink_RXB.IsEnabled = false;
            }
        }

        private void rbRepost_Checked(object sender, RoutedEventArgs e)
        {
            if (rbRepost.IsChecked.Value == true)
            {
                labelPostText.Content = "---:";
                tboxMainBody_XRIBT.IsEnabled = false;
                labelQuoteLink.IsEnabled = true;
                labelQuoteLink.Content = "Repost Link";
                tboxQuoteLink_RXB.IsEnabled = true;
            }
        }

        private void tickRoption_Checked(object sender, RoutedEventArgs e)
        {
            if (tickRoption.IsChecked.Value == false)
            {
                labelPostTitle.Content = "---";
                tboxTitle_R.IsEnabled = false;
            }
            else if (tickRoption.IsChecked.Value == true)
            {
                labelPostTitle.Content = "Post Title";
                tboxTitle_R.IsEnabled = true;
            }
        }
    }
}