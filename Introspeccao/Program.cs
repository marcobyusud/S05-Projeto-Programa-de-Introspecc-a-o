// Criatividade adicional: menu visual, frases motivacionais,
// contagem de sessoes e registro das atividades realizadas.

namespace Introspeccao;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            ExibirCabecalho("PAUSA CONSCIENTE");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  Respire, reflita e redescubra o que existe de bom em voce.");
            Console.ResetColor();
            Console.WriteLine();
            ExibirSessoesRealizadas();
            Console.WriteLine();
            Console.WriteLine("  Sua jornada hoje:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  [1]  Respirar  - desacelere o corpo");
            Console.WriteLine("  [2]  Refletir  - reconheca sua forca");
            Console.WriteLine("  [3]  Listar    - encontre o que importa");
            Console.ResetColor();
            Console.WriteLine("  [4]  Encerrar");
            Console.WriteLine();
            Console.Write("Escolha uma opcao: ");

            string? opcao = Console.ReadLine();
            Atividade? atividade = opcao switch
            {
                "1" => new AtividadeDeRespiracao(),
                "2" => new AtividadeDeReflexao(),
                "3" => new AtividadeDeListagem(),
                "4" => null,
                _ => null
            };

            if (opcao == "4")
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  Obrigado por reservar um tempo para voce.");
                Console.ResetColor();
                break;
            }

            if (atividade == null)
            {
                Console.WriteLine("Opcao invalida. Tente novamente.");
                Thread.Sleep(1500);
                continue;
            }

            atividade.Executar();
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para voltar ao menu.");
            Console.ReadLine();
        }
    }

    private static void ExibirCabecalho(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  ==================================");
        Console.WriteLine($"       {titulo}");
        Console.WriteLine("  ==================================");
        Console.ResetColor();
    }

    private static void ExibirSessoesRealizadas()
    {
        int quantidade = 0;
        try
        {
            if (File.Exists("registro.txt"))
            {
                quantidade = File.ReadAllLines("registro.txt").Length;
            }
        }
        catch
        {
            quantidade = 0;
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write($"  Progresso: ");
        Console.ResetColor();
        Console.WriteLine($"{quantidade} atividade(s) concluida(s)");
    }
}
