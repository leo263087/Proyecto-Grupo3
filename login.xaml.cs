using static Proyecto_Grupo3.datos;

namespace Proyecto_Grupo3;

public partial class Login : ContentPage
{
    usuario usuario = new usuario();
    public Login()
    {
        InitializeComponent();
    }

    private void Clicked(object sender, EventArgs e)
    {
        if (sender is Button)
        {
            Button btn = (Button)sender;
            if (btn.Id == BtnLogin.Id)
            {
                if (string.IsNullOrEmpty(EUser.Text) || string.IsNullOrEmpty(EPassword.Text))
                {
                    DisplayAlert("Error", "Usuario o Contraseña vacios", "Ok");
                }
                else if (EUser.Text !=usuario.nombre || EPassword.Text != usuario.clave.ToString()) 
                {
                    DisplayAlert("Error", "Usuario o Contraseña incorrectos", "Ok");
                }
                else
                {
                    App.Current!.MainPage = new AppShell();
                }
            }
          
        }
        else if (sender is ImageButton)
        {
            ImageButton btn = (ImageButton)sender;
            if (btn.Id == ImgBtnShowPassword.Id)
            {
                if (btn.Source.ToString().Contains("ic_visibility_off.png"))
                {
                    btn.Source = ImageSource.FromFile("ic_remove_red_eye.png");
                    EPassword.IsPassword = false;
                }
                else
                {
                    btn.Source = ImageSource.FromFile("ic_visibility_off.png");
                    EPassword.IsPassword = true;
                }
            }
        }


    }

}