﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;
using System.Windows.Media.Imaging;
using ModelsShared;
using System.IO;
using System;

namespace TrireksaApp.Contents.Penjualan
{
    public class PenjualanInfoVM : ViewModelBase
    {
        private Manifestoutgoing _manSelect;
        private BitmapImage _qr;
        private ModelsShared.Photo _photo;

        public ModelsShared.Models.Penjualan PenjualanItem { get; set; }
        public ObservableCollection<Manifestoutgoing> ManifestSource { get; set; }
        public CollectionView ManifestView { get; private set; }
        public Manifestoutgoing ManifestSelectedItem
        {
            get { return _manSelect; }
            set
            {
                _manSelect = value;
                OnPropertyChanged("ManifestSelectedItem");
            }
        }
        public ModelsShared.Models.Invoice InvoiceStatusView { get; private set; }
        public BitmapImage STTQRCode
        {
            get
            {

                return _qr;
            }
            set
            {
                _qr = value;

                OnPropertyChanged("STTQRCode");
            }
        }
        public List<ModelsShared.Photo> Galeries { get; private set; }
        public CollectionView GaleriesView { get; set; }

        public PenjualanInfoVM(ModelsShared.Models.Penjualan selectedItem)
        {
            Galeries = new List<ModelsShared.Photo>();
            GaleriesView = (CollectionView)CollectionViewSource.GetDefaultView(Galeries);
            this.PenjualanItem = selectedItem;
            STTQRCode = Helper.GenerateQRCode(selectedItem.STT.ToString(), 100);
            this.ManifestSource = new ObservableCollection<ModelsShared.Models.Manifestoutgoing>();
            this.ManifestView = (CollectionView)CollectionViewSource.GetDefaultView(ManifestSource);
            SetManifest(selectedItem);
            SetInvoice(selectedItem);
            GetThumbs(selectedItem);

            AddNewPicture = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddNewPictureAction };
            PrintPicture = new CommandHandler { CanExecuteAction = x => SelectedPhoto != null, ExecuteAction = PrintPictureAction };
            DeletePicture = new CommandHandler { CanExecuteAction = x => SelectedPhoto != null, ExecuteAction = DeletePictureAction };


        }

        private async void DeletePictureAction(object obj)
        {
            var context = ResourcesBase.GetMainWindowViewModel().PenjualanCollection;
            var isDeleted = await context.DeletePhoto(SelectedPhoto);
            if(isDeleted)
            {
                Galeries.Remove(SelectedPhoto);
                GaleriesView.Refresh();
            }


        }

        private void PrintPictureAction(object obj)
        {

            ResourcesBase.PrintPreview("Print Photo", "TrireksaApp.Reports.Layouts.PrintImageLayout.rdlc", 
                new Microsoft.Reporting.WinForms.ReportDataSource { Value = new List<ModelsShared.Photo> { SelectedPhoto } },null);
        }

        private async void AddNewPictureAction(object obj)
        {
            ModelsShared.Photo ph = new ModelsShared.Photo();
            var dialogresult= ResourcesBase.ShowOpenFileDialog();
            string filename = dialogresult.FileName;
            if(!string.IsNullOrEmpty(filename))
            {
                using (var stream = new MemoryStream())
                {
                    var file = File.Open(filename, FileMode.Open);
                    file.CopyTo(stream);
                    file.Close();
                    ph.Picture = stream.ToArray();
                }
                ph.PenjualanId = PenjualanItem.Id;
                ph.Ext = filename.Split('.')[1];
                var context = ResourcesBase.GetMainWindowViewModel().PenjualanCollection;
                var res = await context.AddNewPhoto(ph);
                if (res != null)
                {
                    Galeries.Add(res);
                    GaleriesView.Refresh();
                    SelectedPhoto = res;
                }
            }
        }

        private async void GetThumbs(ModelsShared.Models.Penjualan selectedItem)
        {
            var context = ResourcesBase.GetMainWindowViewModel().PenjualanCollection;
            var result = await context.GetPhotoByPenjualanId(selectedItem.Id);
            if (result != null)
            {
                foreach (var item in result)
                    Galeries.Add(item);

                GaleriesView.Refresh();
                
            }

        }
        

        public ModelsShared.Photo SelectedPhoto
        {
            get
            {
                return _photo;
            }
            set
            {
                _photo = value;
                if (_photo != null && _photo.Picture == null)
                    GetPictureById(_photo);
                OnPropertyChanged("SelectedPhoto");
            }
        }

        private async void GetPictureById(ModelsShared.Photo photo)
        {
            var context = ResourcesBase.GetMainWindowViewModel().PenjualanCollection;
            photo.Picture = await context.GetPictureById(photo.Id);
        }

        public CommandHandler AddNewPicture { get; }
        public CommandHandler PrintPicture { get; private set; }
        public CommandHandler DeletePicture { get; private set; }

        private async void SetInvoice(ModelsShared.Models.Penjualan selected)
        {
            var context = ResourcesBase.GetMainWindowViewModel().InvoiceCollections;
            var x = await context.GetInvoiceForPenjualanInfo(selected.Id);
            if (x != null)
            {
                this.InvoiceStatusView = new ModelsShared.Models.Invoice();
                this.InvoiceStatusView.Number = x.Number;
                this.InvoiceStatusView.CreateDate = x.CreateDate;
                this.InvoiceStatusView.IsDelivery = x.IsDelivery;
                this.InvoiceStatusView.InvoiceStatus = x.InvoiceStatus;
            }
        }

        public async void SetManifest(ModelsShared.Models.Penjualan p)
        {
            var result = await MainVM.ManifestOutgoingCollection.ManifestsByPenjualanId(p.Id);
            if (result != null)
            {
                ManifestSource.Clear();
                foreach (var item in result)
                {
                    var m = await MainVM.ManifestOutgoingCollection.GetItemById(item.Id);
                    this.ManifestSource.Add(m);
                }
                this.ManifestView.Refresh();
            }

        }
    }
}
