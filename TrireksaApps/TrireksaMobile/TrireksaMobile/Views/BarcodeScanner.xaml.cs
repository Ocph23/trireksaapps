﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrireksaMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace TrireksaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeScanner : ContentPage
    {
        public BarcodeScanner()
        {
            InitializeComponent();
            scanView.Options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    ZXing.BarcodeFormat.EAN_8, BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE
                },
                DelayBetweenContinuousScans = 100,
                AutoRotate = false, 
            };
            
            scanView.AutoFocus(100,100);
            BindingContext = new BarcodeScannerViewModel();
        }

        private void zXingDefaultOverlay_FlashButtonClicked(Button sender, EventArgs e)
        {
            scanView.IsTorchOn = !scanView.IsTorchOn;
        }
    }


    public  delegate void OnResultBarcode(object obj);

    internal class BarcodeScannerViewModel : BaseViewModel
    {

        public event OnResultBarcode OnResultScanHandler;

        public BarcodeScannerViewModel()
        {
            ScanningCommand = new Command(ScanningAction, x => IsScanning);
            ScanAgainCommand = new Command(() => { IsScanning = true; ScanAgain = false; TextResult = string.Empty; });
            TakeCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    OnResultScanHandler?.Invoke(TextResult);
                    await Task.Delay(1000);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });

            });

            CancalCommand = new Command(() =>
            {
                TextResult = string.Empty;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(1000);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            });

            Task.Run(() => StartScan());
        }



        private string textResult;

        public string TextResult
        {
            get { return textResult; }
            set { SetProperty(ref textResult, value); }
        }


        private bool scanAgain;

        public bool ScanAgain
        {
            get { return scanAgain; }
            set { SetProperty(ref scanAgain, value); }
        }



        public async Task StartScan()
        {
            await Task.Delay(1000);
            IsScanning = true;
        }




        private void ScanningAction(object obj)
        {
            IsScanning = false;
            ScanAgain = true;
            Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(100);
                    try
                    {
                        IsBusy = true;
                        var data = obj as Result;
                        TextResult = data.Text;
                        OnResultScanHandler?.Invoke(TextResult);
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        Console.WriteLine(obj.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });

        }

        internal void Close()
        {
            Shell.Current.Navigation.PopModalAsync();
        }

        public Command ScanningCommand
        {
            get
            {
                return _scanningCommand;
            }
            set
            {
                SetProperty(ref _scanningCommand, value);
            }
        }

        public Command ScanAgainCommand { get; private set; }
        public Command TakeCommand { get; }
        public Command CancalCommand { get; }

        private bool isScaning;

        public bool IsScanning
        {
            get { return isScaning; }
            set { SetProperty(ref isScaning, value); }
        }

        public bool IsAnalyzing
        {
            get { return isAnalyzing; }
            set { SetProperty(ref isAnalyzing, value); }
        }


        private Command _scanningCommand;
        private bool isAnalyzing;

    }
}