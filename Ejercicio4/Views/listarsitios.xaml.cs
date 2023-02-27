using Ejercicio4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listarsitios : ContentPage
    {
        private sitios sitio;
        public listarsitios()
        {
            InitializeComponent();
        }

        private void liestasistios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sitio = (sitios)e.Item;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listasitios.ItemsSource = await App.BaseDatos.ObtenerlistadoSitio();
        }



        private async void btneliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var eliminar = await App.BaseDatos.eliminarsitio(sitio);


                if (eliminar != 0)
                {
                    await DisplayAlert("Aviso", "Sitio eliminado !", "Ok");
                    listasitios.ItemsSource = await App.BaseDatos.ObtenerlistadoSitio();

                }
                else
                {
                    await DisplayAlert("Aviso", "Ha ocurrido un error !", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Aviso", "Favor seleccione que sitio desea eliminar", "Ok");
            }



        }


    }
}