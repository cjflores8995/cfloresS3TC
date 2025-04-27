using cfloresS3TC.Helpers;

using System.Globalization;

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

        string seleccion = pickerTipoIdentificacion.SelectedItem.ToString();

        if(seleccion == "Cédula")
        {
            txtIdentificacion.TextChanged -= TextIdentificacion_TextChanged;
            txtIdentificacion.TextChanged += TextIdentificacion_TextChanged;
        }
        else if(seleccion == "RUC")
        {
            txtIdentificacion.TextChanged -= TextIdentificacion_TextChanged;
            txtIdentificacion.TextChanged += TextIdentificacion_TextChanged;
        } 
        else if (seleccion == "Pasaporte")
        {
            txtIdentificacion.TextChanged -= TextIdentificacion_TextChanged;
            txtIdentificacion.TextChanged += TextIdentificacion_TextChanged;
        }

        txtIdentificacion.Focus();
    }

    

    #region Private Methods

    private void ValidarDecimales()
    {
        MetodosDeAyuda.ValidarNumericosDecimales(txtSalario);
    }

    private async Task MostrarProceso(List<VisualElement> camposInvalidos)
    {
        bool emailValido = EsEmailValido(txtEmail.Text);

        if (camposInvalidos.Any() || !emailValido)
        {
            string mensajeError = "Completa todos los campos requeridos";

            if (!emailValido)
            {
                mensajeError = "Ingresa un email válido";
            }

            await DisplayAlert("Error", mensajeError, "Aceptar");
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
            string strTipoIdentificacion = pickerTipoIdentificacion.SelectedItem.ToString();
            string strIdentificacion = txtIdentificacion.Text;
            string strNombres = txtNombres.Text;
            string strApellidos = txtApellidos.Text;
            string strFecha = datePickerFecha.Date.ToString("dd/MM/yyyy");
            string strEmail = txtEmail.Text;
            string strSalario = txtSalario.Text;
            string strAporteIess = MetodosDeAyuda.CalcularAporteIsess(strSalario).ToString();

            await Navigation.PushAsync(new vResultado(
                strTipoIdentificacion,
                strIdentificacion,
                strNombres,
                strApellidos,
                strFecha,
                strEmail,
                strSalario,
                strAporteIess
            ));
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Algo salio mal!: {ex.Message}", "Aceptar");
        }
    }

    private void TextIdentificacion_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (pickerTipoIdentificacion.SelectedItem == null)
            return;

        string seleccion = pickerTipoIdentificacion.SelectedItem.ToString();
        string texto = e.NewTextValue ?? string.Empty;

        if (seleccion == "Cédula")
        {
            string nuevoTexto = new string(texto.Where(char.IsDigit).ToArray());
            if (nuevoTexto.Length > 10)
                nuevoTexto = nuevoTexto.Substring(0, 10);
            txtIdentificacion.Text = nuevoTexto;
        }
        else if (seleccion == "RUC")
        {
            string nuevoTexto = new string(texto.Where(char.IsDigit).ToArray());
            if (nuevoTexto.Length > 13)
                nuevoTexto = nuevoTexto.Substring(0, 13);

            if (nuevoTexto.Length == 13)
            {
                if (!nuevoTexto.EndsWith("001"))
                {
                    nuevoTexto = nuevoTexto.Substring(0, 10) + "001";
                }
            }
            txtIdentificacion.Text = nuevoTexto;
        }
        else if (seleccion == "Pasaporte")
        {
            if (texto.Length > 20)
                texto = texto.Substring(0, 20);
            txtIdentificacion.Text = texto;
        }

        txtIdentificacion.CursorPosition = txtIdentificacion.Text.Length;
    }

    private bool EsEmailValido(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Expresión regular básica para emails
        var regex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }

    #endregion Private Methods
}