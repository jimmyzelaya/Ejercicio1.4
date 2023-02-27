using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ejercicio4.Models;
using Ejercicio4.Views;

namespace Ejercicio4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        byte[] imageToSave;
        private async void AddImg(object sender, EventArgs e)
        {
            try
            {

                var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoApp",
                    Name = DateTime.Now.ToString() + "_foto.jpg",
                    SaveToAlbum = true
                });

                await DisplayAlert("Ubicacion de la foto: ", takepic.Path, "Aceptar");

                if (takepic != null)
                {
                    imageToSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    takepic.GetStream().CopyTo(memoryStream);
                    imageToSave = memoryStream.ToArray();

                    img.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
                }
                
                txtdescripcion.Focus();//le pasamos el puntero a la descripcion
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Se ha generado el siguiente error al agregar la imagen " + ex, "Aceptar");
            }
        }


        private async void btnagregar_Clicked(object sender, EventArgs e)
        {
            if (imageToSave == null)
            {
                await DisplayAlert("AVISO", "Capture una imagen del sitio", "OK");
            }
            else if (txtdescripcion.Text == null)
            {
                await DisplayAlert("AVISO", "Ingrese la descripcion del sitio", "OK");
            }
            else
            {
                var sitio = new sitios { imagen = imageToSave, nombre = txtnombre.Text, descripcion = txtdescripcion.Text };
                var resultado = await App.BaseDatos.sitioSave(sitio);

                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "¡Sitio ingresado con exito!", "OK");
                    txtdescripcion.Text = "";
                    img.Source = "foto.jpg";
                    imageToSave = null;

                }
                else
                {
                    await DisplayAlert("Aviso", "Ha Ocurrido un Error", "OK");
                }


                await Navigation.PopAsync();
            }

        }

        private async void btnlistar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new listarsitios());
        }


    }
}
