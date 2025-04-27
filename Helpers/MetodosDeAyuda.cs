using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfloresS3TC.Helpers
{
    public static class MetodosDeAyuda
    {
        /// <summary>
        /// Valida que se púedan ingresar numeros entre 0 y 10
        /// </summary>
        /// <param name="entry"></param>
        public static void ValidarNumericosDecimales(Entry entry)
        {
            string separadorDecimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            entry.Keyboard = Keyboard.Numeric;

            entry.TextChanged += (s, e) =>
            {
                string nuevoTexto = entry.Text;

                if (nuevoTexto == e.OldTextValue)
                    return;

                if (string.IsNullOrWhiteSpace(nuevoTexto))
                    return;

                string textoNormalizado = separadorDecimal == "," ?
                    nuevoTexto.Replace(".", ",") :
                    nuevoTexto.Replace(",", ".");

                if (!double.TryParse(textoNormalizado, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out double valorNumerico))
                {
                    entry.Text = e.OldTextValue;
                    return;
                }
            };
        }

        /// <summary>
        /// Obtiene un listado de los campos invalidos
        /// </summary>
        /// <param name="camposTexto"></param>
        /// <param name="camposPicker"></param>
        /// <returns></returns>
        public static List<VisualElement> ObtenerCamposInvalidos(List<Entry> camposTexto, List<Picker> camposPicker)
        {
            var camposInvalidos = new List<VisualElement>();

            camposInvalidos.AddRange(camposTexto.Where(c => string.IsNullOrEmpty(c.Text)));
            camposInvalidos.AddRange(camposPicker.Where(p => p.SelectedItem == null));

            return camposInvalidos;
        }

        /// <summary>
        /// Calcular aporte IESS
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static double CalcularAporteIsess(string valor)
        {
            string separadorDecimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            valor = separadorDecimal == "," ? valor.Replace(".", ",") : valor.Replace(",", ".");

            if (double.TryParse(valor, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out double valorNumerico))
            {
                return Math.Round(valorNumerico * 9.45 / 100, 2);
            }

            return 0;
        }
    }
}
