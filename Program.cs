using giovani_d3_avaliacao.Models;
using giovani_d3_avaliacao.Repositories;

namespace giovani_d3_avaliacao
{
    internal class Program
    {
        private const string path = "database/log.txt";
        static void Run(UserRepository user)
        {
            bool loggedIn = true;
            do
            {
                Console.WriteLine("\nEscolha uma das opções abaixo:\n");
                Console.WriteLine("1 - Deslogar");
                Console.WriteLine("2 - Encerrar o sistema");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        user.LogOut();
                        loggedIn = false;
                        break;

                    case "2":
                        user.ShutDown();                       
                        break;

                    default:
                        break;
                }

            } while(loggedIn);               
        }

        static void Main(string[] args)
        {
            UserRepository _user = new();
            LogRepository _log = new(path);

            //_user.LoadData();

            string option;

            do
            {
                Console.WriteLine("\nEscolha uma das opções abaixo:\n");
                Console.WriteLine("1 - Cadastrar-se no sistema");
                Console.WriteLine("2 - Acessar o sistema");
                Console.WriteLine("3 - Deletar usuário");
                Console.WriteLine("4 - Cancelar");

                option = Console.ReadLine();

                string? id;
                string? pwd;
                string? email;
                string? name;

                switch (option)
                {
                    case "1":
                        
                        Console.WriteLine("\nDigite um ID de usuário");
                        id = Console.ReadLine();

                        Console.WriteLine("\nDigite o nome de usuário");
                        name = Console.ReadLine();

                        Console.WriteLine("\nDigite o email do usuário");
                        email = Console.ReadLine();

                        Console.WriteLine("\nDigite a senha do usuário");
                        pwd = Console.ReadLine();

                        User newUser = new()
                        {
                            IdUser = id,
                            Name = name,
                            Email = email,
                            Password = pwd
                        };

                        if (_user.Create(newUser) != 1) Console.WriteLine("\nOperação não realizada, verifique os dados inseridos!");
                        break;

                    case "2":
                        Console.WriteLine("\nDigite o ID de usuário");
                        id = Console.ReadLine();

                        Console.WriteLine($"\nDigite a senha para {id}");
                        pwd = Console.ReadLine();

                        Console.WriteLine($"\nDigite o email para {id}");
                        email = Console.ReadLine();

                        if (!_user.Access(id, email, pwd)) Console.WriteLine("\nOperação não realizada, verifique os dados inseridos!");

                        else
                        {
                            name = _user.GetUserName(id);
                            User user = new()
                            {
                                IdUser = id,
                                Name = name,
                                Email = email,
                                Password = pwd
                            };

                            _log.RegisterAccess(user);
                            Program.Run(_user);  
                        } 

                        break;
                    
                    case "3":

                        Console.WriteLine("\nDigite o ID do usuário a ser excluído");
                        id = Console.ReadLine();

                        Console.WriteLine($"\nDigite a senha para {id}");
                        pwd = Console.ReadLine();

                        if (_user.Delete(id, pwd) != 1) Console.WriteLine("\nOperação não realizada, verifique os dados inseridos!");
                        break;

                    case "4":
                        _user.Cancel();
                        break;

                    default:
                        break;
                }

            } while (true);
        }
    }
}
