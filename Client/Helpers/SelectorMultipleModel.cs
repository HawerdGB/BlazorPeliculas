namespace BlazorPeliculas.Client.Helpers
{
    public class SelectorMultipleModel
    {
        public SelectorMultipleModel(string llave, string valor)
        {
            Valor = valor;
            Llave = llave;
        }
        public string Llave { get; set; }
        public string Valor { get; set; }
    }
}
