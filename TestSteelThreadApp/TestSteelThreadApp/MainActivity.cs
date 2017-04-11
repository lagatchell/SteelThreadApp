using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace TestSteelThreadApp
{
    [Activity(Label = "TestSteelThreadApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            TextView myText = FindViewById<TextView>(Resource.Id.MyText);

            using (var client = new HttpClient())
            {
                var url = string.Format("https://lukeproject.azurewebsites.net/api/ReceiveData?code=nDYOtuYaJ4emCUpfKacDcLUJDD/eCWf8ROFch2CHTuct5e0Q5gtXHg==");

                var content = new StringContent("{name: '" + myText.Text + "'}", Encoding.UTF8, "application/json");

                var result = client.PostAsync(url, content).Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;

                Toast.MakeText(Application.Context, JsonConvert.DeserializeObject<string>(result), ToastLength.Long).Show();

            }
        }
    }
}

