using System.Collections.Generic; 
    public class Producto
    {
       
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; } 
        public Categoria Categoria { get; set; }
        public Dictionary<string, string> PropiedadesEspecificas { get; private set; }
    public Producto()
    {
        PropiedadesEspecificas = new Dictionary<string, string>();
    }
       
        public Producto(string id, string nombre, string descripcion, int cantidad, float precio, Categoria categoria)
        {
            ID = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            Categoria = categoria;
            PropiedadesEspecificas = new Dictionary<string, string>(); 


        // Método para agregar propiedades específicas
        public void AgregarPropiedadEspecifica(string clave, string valor)
        {
            if (PropiedadesEspecificas == null) // Asegurarse de que esté inicializado
            {
                PropiedadesEspecificas = new Dictionary<string, string>();
            }
            PropiedadesEspecificas[clave] = valor;
        }
        public override string ToString()
        {
            
            return $"{Nombre} (ID: {ID}) | Cant: {Cantidad} | Precio: {Precio:C} | Cat: {categoriaNombre}";
        }
    }
