using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Laboratorio4
{
    public partial class Form1 : Form
    {
        // Lista utilizada para recibir los productos que vienen desde la base de datos.
        private List<Producto> listaProductos = new List<Producto>();

        // Diccionario utilizado para preparar los datos que se enviarán al INSERT.
        private Dictionary<string, object> myProducto = new Dictionary<string, object>();

        // Guarda el Id del producto seleccionado en el DataGridView.
        private int idSeleccionado = 0;

        // CONSTRUCTOR

        public Form1()
        {
            InitializeComponent();
        }

        // EVENTO LOAD

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarProductos();
        }

        // CARGAR PRODUCTOS EN EL DATAGRIDVIEW

        private void cargarProductos(string filtro = "")
        {
            dgvProductos.Rows.Clear();
            dgvProductos.Refresh();

            listaProductos =
                Conexion.GetProductos(filtro);

            foreach (Producto prod in listaProductos)
            {
                Image img = null;

                if (prod.Imagen != null && prod.Imagen.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(prod.Imagen))
                    {
                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            // Se crea una copia independiente para que la imagen siga existiendo después de cerrar el MemoryStream.
                            img = new Bitmap(bmp);
                        }
                    }
                }

                dgvProductos.Rows.Add(
                    prod.Id,
                    prod.Nombre,
                    prod.Precio,
                    prod.Cantidad,
                    img
                );
            }
        }

        // BUSCAR PRODUCTOS

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            cargarProductos(textBox5.Text.Trim());
        }

        // SELECCIONAR IMAGEN

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar imagen del producto";

                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);

                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        // CONVERTIR IMAGE A BYTE[]
        private byte[] ImageToByteArray(Image image)
        {
            if (image == null)
                return null;

            using (MemoryStream mMemoryStream = new MemoryStream())
            {
                image.Save(
                    mMemoryStream,
                    ImageFormat.Png);

                return mMemoryStream.ToArray();
            }
        }

        // VALIDAR DATOS

        private bool DatosCorrectos()
        {
            // Validar Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el Nombre del Producto.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();

                return false;
            }

            // Validar Precio vacío
            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show(
                    "Ingrese el Precio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrecio.Focus();

                return false;
            }

            // Validar que el Precio sea decimal
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show(
                    "Ingrese un Precio correcto.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrecio.Focus();
                return false;
            }


            // Precio mayor que cero
            if (precio <= 0)
            {
                MessageBox.Show(
                    "El Precio debe ser mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrecio.Focus();

                return false;
            }


            // Validar Cantidad vacía
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show(
                    "Ingrese la Cantidad.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();

                return false;
            }


            // Validar que Cantidad sea int
            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad))
            {
                MessageBox.Show(
                    "Ingrese una Cantidad correcta.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();

                return false;
            }


            // Cantidad no puede ser negativa
            if (cantidad < 0)
            {
                MessageBox.Show(
                    "La Cantidad no puede ser negativa.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();

                return false;
            }

            return true;
        }

        // CARGAR DATOS AL DICCIONARIO
        private void CargarDatosProducto()
        {
            myProducto.Clear();

            myProducto["Nombre"] = txtNombre.Text.Trim();

            myProducto["Precio"] = decimal.Parse(txtPrecio.Text.Trim());

            myProducto["Cantidad"] = int.Parse(txtCantidad.Text.Trim());

            myProducto["Imagen"] = ImageToByteArray(pictureBox1.Image);
        }

        // BOTÓN AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!DatosCorrectos())
                return;

            CargarDatosProducto();

            if (Conexion.InsertSeguro("Productos", myProducto))
            {
                MessageBox.Show(
                    "Producto registrado satisfactoriamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                cargarProductos();

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo registrar el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // SELECCIONAR PRODUCTO EN DATAGRIDVIEW

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita errores si se presiona el encabezado
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

            // Folio visual = Id de la Base de Datos
            idSeleccionado = Convert.ToInt32(fila.Cells[0].Value);

            txtFolio.Text = idSeleccionado.ToString();

            txtNombre.Text = fila.Cells[1].Value?.ToString();

            txtPrecio.Text = fila.Cells[2].Value?.ToString();

            txtCantidad.Text = fila.Cells[3].Value?.ToString();


            // Recuperar imagen
            if (fila.Cells[4].Value is Image img)
            {
                pictureBox1.Image = new Bitmap(img);

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        // BOTÓN MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un producto para modificar.",
                    "Modificar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            if (!DatosCorrectos())
                return;


            Producto producto = new Producto();

            producto.Id = idSeleccionado;

            producto.Nombre = txtNombre.Text.Trim();

            producto.Precio = decimal.Parse(txtPrecio.Text.Trim());

            producto.Cantidad = int.Parse(txtCantidad.Text.Trim());

            producto.Imagen = ImageToByteArray(pictureBox1.Image);


            if (Conexion.ModificarProducto(producto))
            {
                MessageBox.Show(
                    "Producto modificado satisfactoriamente.",
                    "Modificación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                cargarProductos();

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo modificar el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // BOTÓN ELIMINAR

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un producto para eliminar.",
                    "Eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Yes)
            {
                if (Conexion.EliminarProducto(idSeleccionado))
                {
                    MessageBox.Show(
                        "Producto eliminado satisfactoriamente.",
                        "Eliminación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    cargarProductos();

                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el producto.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // LIMPIAR CAMPOS

        private void LimpiarCampos()
        {
            txtFolio.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();

            pictureBox1.Image = null;

            idSeleccionado = 0;

            // Limpiar selección del grid
            dgvProductos.ClearSelection();

            txtNombre.Focus();
        }

        // BOTÓN LIMPIAR

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // BOTÓN SALIR

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta =
                MessageBox.Show(
                    "¿Desea salir de la aplicación?",
                    "Salir",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Cerrando aplicación...",
                    "Salir",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Application.Exit();
            }
        }
    }
}