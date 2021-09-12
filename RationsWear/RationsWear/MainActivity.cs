using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Wearable.Activity;
using Java.Interop;
using Android.Views.Animations;
using Android.Support.Wear.Widget;

namespace RationsWear
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;
        TextView progressText;
        Button btnGo;
        Button btnWhen;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            textView = FindViewById<TextView>(Resource.Id.text);
            btnGo = FindViewById<Button>(Resource.Id.button1);
            btnWhen = FindViewById<Button>(Resource.Id.button2);

            textView.Visibility = Android.Views.ViewStates.Invisible;

            // Create days left
            DateTime endDate = new DateTime(2020, 10, 1);
            Double remainingDays = (endDate - DateTime.Now).TotalDays;

            btnGo.Click += delegate
            {
                if (remainingDays > 0)
                {
                    this.StartConfirmationActivity(ConfirmationActivity.FailureAnimation, string.Format("{0} days for Pandemic to end", Math.Round(remainingDays)));
                }
                else
                {
                    this.StartConfirmationActivity(ConfirmationActivity.SuccessAnimation, string.Format("It's over.", remainingDays));
                }
            };

            btnWhen.Click += delegate
            {
                this.StartConfirmationActivity(ConfirmationActivity.SuccessAnimation, string.Format("{0}", endDate.ToShortDateString()));
            };


            SetAmbientEnabled();
        }

        private void StartConfirmationActivity(int animationType, string Message)
        {
            Intent confirmationActivity = new Intent(this, typeof(ConfirmationActivity))
                .SetFlags(ActivityFlags.NewTask | ActivityFlags.NoAnimation)
                .PutExtra(ConfirmationActivity.ExtraAnimationType, animationType)
                .PutExtra(ConfirmationActivity.ExtraMessage, Message);
            StartActivity(confirmationActivity);
        }

    }
}

