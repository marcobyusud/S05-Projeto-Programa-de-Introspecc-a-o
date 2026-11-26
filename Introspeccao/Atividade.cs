namespace Introspeccao;

public abstract class Atividade
{
    private readonly string _nome;
    private readonly string _descricao;
    private int _duracao;

    protected Atividade(string nome, string descricao)
    {
        _nome = nome;
        _descricao = descricao;
    }

    public abstract void Executar();

    protected void ExibirMensagemInicial()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  === {_nome.ToUpper()} ===");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(_descricao);
        Console.WriteLine();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Quanto tempo voce quer reservar? ");
            Console.ResetColor();
            Console.Write("Duracao em segundos: ");
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int duracao) && duracao > 0)
            {
                _duracao = duracao;
                break;
            }

            Console.WriteLine("Por favor, informe um numero inteiro maior que zero.");
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Prepare-se para comecar...");
        Console.ResetColor();
        ExibirContagemRegressiva(3);
    }

    protected void ExibirMensagemFinal()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  * Muito bem! Voce concluiu a atividade. *");
        Console.ResetColor();
        ExibirContagemRegressiva(3);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  Resumo: {_nome}");
        Console.WriteLine($"  Tempo dedicado: {_duracao} segundos");
        Console.ResetColor();
        ExibirContagemRegressiva(3);

        try
        {
            string linha = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {_nome} - {_duracao} segundos";
            File.AppendAllLines("registro.txt", new[] { linha });
        }
        catch
        {
            Console.WriteLine("Nao foi possivel salvar o registro da atividade.");
        }
    }

    protected int ObterDuracao()
    {
        return _duracao;
    }

    public void ExibirProgresso(int segundos)
    {
        char[] caracteres = { '|', '/', '-', '\\' };
        int indice = 0;

        for (int i = 0; i < segundos * 4; i++)
        {
            Console.Write(caracteres[indice]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            indice = (indice + 1) % caracteres.Length;
        }
    }

    public void ExibirContagemRegressiva(int segundos)
    {
        for (int numero = segundos; numero > 0; numero--)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\r  [{new string('#', segundos - numero + 1)}{new string('.', numero - 1)}] {numero}s ");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        Console.Write("\r" + new string(' ', 40) + "\r");
    }

    protected void ExibirBarraDeTempo(DateTime inicio, DateTime fim)
    {
        double percentual = Math.Clamp((DateTime.Now - inicio).TotalMilliseconds /
            (fim - inicio).TotalMilliseconds, 0, 1);
        int largura = 30;
        int preenchidos = (int)(largura * percentual);

        Console.Write($"\r  [{new string('#', preenchidos)}{new string('-', largura - preenchidos)}] ");
        Console.Write($"{(int)(percentual * 100):000}%");
    }
}
