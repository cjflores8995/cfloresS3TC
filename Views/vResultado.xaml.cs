using System.Text;

namespace cfloresS3TC.Views;

public partial class vResultado : ContentPage
{
    public vResultado()
	{
		InitializeComponent();
	}

    public vResultado(
		string tipoIdentificacion,
        string identificacion,
		string nombres,
		string apellidos,
		string fecha,
		string email,
		string salario,
		string aporteIess)
	{


		InitializeComponent();
        lblTipoIdentificacion.Text = tipoIdentificacion;
        lblIdentificacion.Text = identificacion;
		lblNombres.Text = nombres;
		lblApellidos.Text = apellidos;
		lblFecha.Text = fecha;
		lblEmail.Text = email;
		lblSalario.Text = salario;
		lblAporteIess.Text = aporteIess;
}

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        await GuardarArchivo();
    }

    private async Task GuardarArchivo()
    {
        string rutaCarpeta = Path.Combine("C:\\", "salida");
        if (!Directory.Exists(rutaCarpeta))
        {
            Directory.CreateDirectory(rutaCarpeta);
        }


        string strTipoIdentificacion = lblTipoIdentificacion.Text;
        string strIdentificacion = lblIdentificacion.Text;
        string strNombres = lblNombres.Text;
        string strApellidos = lblApellidos.Text;
        string strFecha = lblFecha.Text;
        string strEmail = lblEmail.Text;
        string strSalario = lblSalario.Text;
        string strAporteIess = lblAporteIess.Text;

        string nombreArchivo = $"respuesta_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

        var datos = new StringBuilder();
        datos.AppendLine($"Tipo Identificación: {strTipoIdentificacion}");
        datos.AppendLine($"Identificación: {strIdentificacion}");
        datos.AppendLine($"Nombres: {strNombres}");
        datos.AppendLine($"Apellidos: {strApellidos}");
        datos.AppendLine($"Fecha: {strFecha}");
        datos.AppendLine($"Email: {strEmail}");
        datos.AppendLine($"Salario: {strSalario}");
        datos.AppendLine($"Aporte IESS: {strAporteIess}");

        File.WriteAllText(rutaArchivo, datos.ToString());

        await DisplayAlert("Éxito", $"Datos almacenados en: {rutaArchivo}", "Aceptar");
    }
}