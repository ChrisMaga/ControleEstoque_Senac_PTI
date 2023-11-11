//Estoque para um Armarinho de Aviamento
using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

class Program
{
    
    private static void Main(string[] args)
    {
        Console.WriteLine("ARMARINHOS E AVIAMENTOS --- CONTROLE DE ESTOQUE\n");
        menuSoftware();
        
    }
    static void menuSoftware()
    {
        Estoque estoque = new Estoque();
        string opcaoMenu;
        do
            {
                Console.WriteLine("---- Escolha uma das opções: ----");
                Console.WriteLine("[1] Novo");
                Console.WriteLine("[2] Listar Produtos");
                Console.WriteLine("[3] Remover Produtos");
                Console.WriteLine("[4] Entrada Estoque");
                Console.WriteLine("[5] Saída Estoque");
                Console.WriteLine("[0] Sair");

                opcaoMenu = Console.ReadLine();

                switch (opcaoMenu)
                {
                    //Cadastrar os produtos
                    case "1":
                        estoque.Novo();
                        break;
                    //Listar os produtos
                    case "2":
                        estoque.Listar();
                        break;
                    //Remover Produto
                    case "3":
                        estoque.Remover();
                        break;
                    //Entrada de Estoque
                    case "4":
                        estoque.Entrada();
                        break;
                    //Saída de Estoque
                    case "5":
                        estoque.Saída();
                        break;
                    //Sair do programa
                    case "0":
                        Console.WriteLine("Programa encerrado. Obrigado!\n");
                        Console.WriteLine("Pressione qualquer tecla para sair.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Escolha uma das opções acima");
                        break;
                }

            } while (opcaoMenu != "0");
        }

    public class Produto
    {
        private static int proximoIndice = 1;

        public int Indice { get; }
        public string Item { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public int Peso { get; set; }
        public int Lote { get; set; }
        public double Preco { get; set; }
        public int Qtd_estoque { get; set; }

        public Produto(string item, string marca, string cor, int peso, int lote, double preco)
        {
            this.Indice = proximoIndice++;
            this.Item = item;
            this.Marca = marca;
            this.Cor = cor;
            this.Peso = peso;
            this.Lote = lote;
            this.Preco = preco;
        }

        public string getItem() { return this.Item; }
        public void SetEstoque(int quantidade) { this.Qtd_estoque = quantidade; }
        public int getQtd_estoque() { return this.Qtd_estoque; }

        public override string ToString()
        {
            return $"{this.Indice}.{this.Item}, Marca: {this.Marca}, Cor: {this.Cor}, Peso: {this.Peso}, Lote: {this.Lote}, Preço: ({this.Preco.ToString("C", new CultureInfo("pt-BR"))}) , {this.Qtd_estoque} em estoque.";
        }
    }

    class Estoque
    {
        private List<Produto> produtos;
        public Estoque()
        {
            this.produtos = new List<Produto>();
        }
        public void Novo()
        {
            Console.WriteLine("*---- Digite as informações de seu produto ----*");
            Console.Write("Nome do Produto: ");
            string item = Console.ReadLine();
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Cor: ");
            string cor = Console.ReadLine();
            int peso;
            Console.Write("Peso: ");
            while (!int.TryParse(Console.ReadLine(), out peso))
            {
                Console.WriteLine("Entrada inválida. Digite um número inteiro para o peso:");
            }

            int lote;
            Console.Write("Lote: ");
            while (!int.TryParse(Console.ReadLine(), out lote))
            {
                Console.WriteLine("Entrada inválida. Digite um número inteiro para o lote:");
            }

            double preco;
            Console.Write("Preço: ");
            while (!double.TryParse(Console.ReadLine(), out preco))
            {
                Console.WriteLine("Entrada inválida. Digite um número para o preço:");
            }

            Produto produto = new Produto($"{item}", $"{marca}", $"{cor}", peso, lote, preco);
            this.produtos.Add(produto);
            Console.WriteLine("Produto Adicionado!\n");
        }
        public void Listar()
        {
            Console.WriteLine("*---- LISTA DE PRODUTOS ----*\n");
            if (this.produtos.Count == 0)
            {
                Console.WriteLine("Não há produtos cadastrados.");
            }
            else
            {
                Console.WriteLine($"Produtos cadastrados! {this.produtos.Count}");
                foreach (Produto produto in this.produtos)
                {
                    Console.WriteLine(produto);
                }
            }
        }
        public void Remover()
        {
            Console.WriteLine("*---- REMOÇÃO DE PRODUTOS ----*\n");
            Console.WriteLine("Digite o índice do produto que deseja remover:");

            if (int.TryParse(Console.ReadLine(), out int indiceEscolhido))
            {
                bool localizado = false;

                for (int i = 0; i < this.produtos.Count; i++)
                {
                    if (this.produtos[i].Indice == indiceEscolhido)
                    {
                        this.produtos.RemoveAt(i);
                        Console.WriteLine("Produto removido com sucesso!");
                        localizado = true;
                        break;
                    }
                }

                if (!localizado)
                {
                    Console.WriteLine("Produto não encontrado pelo índice informado.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida para o índice do produto.");
            }
        }

        public void Entrada()
        {
            Console.WriteLine("*---- ENTRADA DE ESTOQUE ----*\n");
            Console.WriteLine("Digite o índice do produto para incluir no estoque:");

            if (int.TryParse(Console.ReadLine(), out int indiceEscolhido))
            {
                bool localizado = false;

                foreach (Produto produto in this.produtos)
                {
                    if (produto.Indice == indiceEscolhido)
                    {
                        Console.WriteLine($"Produto encontrado. Nome: {produto.Item}");
                        Console.WriteLine("Digite a quantidade de estoque: ");
                        if (int.TryParse(Console.ReadLine(), out int quantidade))

                            produto.SetEstoque(produto.getQtd_estoque() + quantidade);

                        Console.WriteLine("Estoque inserido com sucesso!");
                        localizado = true;
                        break;
                    }
                }

                if (!localizado)
                {
                    Console.WriteLine("Produto não encontrado pelo índice informado.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida para o índice do produto.");
            }
        }

        public void Saída()
        {
            Console.WriteLine("*---- SAÍDA DE ESTOQUE ----*\n");
            Console.WriteLine("Digite o índice do produto para retirar do estoque:");

            if (int.TryParse(Console.ReadLine(), out int indiceEscolhido))
            {
                bool localizado = false;

                foreach (Produto produto in this.produtos)
                {
                    if (produto.Indice == indiceEscolhido)
                    {
                        Console.WriteLine($"Produto encontrado. Nome: {produto.Item}");
                        Console.WriteLine("Digite a quantidade de estoque a ser retirada: ");
                        if (int.TryParse(Console.ReadLine(), out int quantidade))
                        {
                            if (quantidade > produto.getQtd_estoque())
                            {
                                Console.WriteLine("Quantidade indisponível no estoque.");
                            }
                            else
                            {
                                produto.SetEstoque(produto.getQtd_estoque() - quantidade);
                                Console.WriteLine("Produto retirado com sucesso!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quantidade inválida. Digite um número inteiro.");
                        }

                        localizado = true;
                        break;
                    }
                }

                if (!localizado)
                {
                    Console.WriteLine("Produto não encontrado pelo índice informado.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida para o índice do produto.");
            }
        }

    }
}