using cfloresS3TC.Helpers;

namespace cfloresS3TC.Views;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
		pickerTipoIdentificacion.ItemsSource = ListadoIdentificacion.Identificaciones;
		ValidarDecimales();

    }

    

    private async void btnProcesar_Clicked(object sender, EventArgs e)
    {
        var camposTexto = new List<Entry> { txtIdentificacion, txtNombres, txtApellidos, txtEmail, txtSalario };
        var campoPicker = new List<Picker> { pickerTipoIdentificacion };

        var camposInvalidos = MetodosDeAyuda.ObtenerCamposInvalidos(camposTexto, campoPicker);

        await MostrarProceso(camposInvalidos);
    }

    private void pickerTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIdentificacion.Text = string.Empty;

        if (pickerTipoIdentificacion.SelectedItem == null)
        {
            return;
        }


            
    }

    #region Private Methods

    private void ValidarDecimales()
    {
        MetodosDeAyuda.ValidarNumericosDecimales(txtSalario);
    }

    private async Task MostrarProceso(List<VisualElement> camposInvalidos)
    {
        if (camposInvalidos.Any())
        {
            await DisplayAlert("Error", "Completa todos los campos requeridos", "Aceptar");
        }
        else
        {
            await EjecutarResultado();
        }
    }

    private async Task EjecutarResultado()
    {
        try
        {
            
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Algo salio mal!: {ex.Message}", "Aceptar");
        }
    }

    #endregion Private Methods
}