namespace Introspeccao;

public class AtividadeDeListagem : Atividade
{
    private readonly Random _aleatorio = new Random();
    private readonly List<string> _itens = new List<string>
    {
        "Quem sao as pessoas que voce aprecia?",
        "Quais sao seus pontos fortes pessoais?",
        "Quem sao as pessoas que voce ajudou esta semana?",
        "Quando voce sentiu o Espirito Santo neste mes?",
        "Quem sao alguns dos seus herois pessoais?"
    };

    private readonly object _travamento = new object();
    private readonly List<string> _listaDoUsuario = new List<string>();

    public AtividadeDeListagem()
        : base(
            "Atividade de Listagem",
            "Esta atividade ajudara voce a refletir sobre as coisas boas da sua vida, fazendo com que voce liste o maximo de coisas que puder em uma determinada area.")
    {
    }

    public override void Executar()
    {
        ExibirMensagemInicial();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  Seu desafio:");
        Console.ResetColor();
        Console.WriteLine(ObterPerguntaAleatoria());

        Console.WriteLine("\nFeche os olhos por alguns segundos e pense na sua lista...");
        ExibirContagemRegressiva(5);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nComece a digitar. Pressione Enter a cada item.");
        Console.WriteLine("O programa mostrara sua quantidade em tempo real.");
        Console.ResetColor();

        _listaDoUsuario.Clear();
        DateTime horaInicio = DateTime.Now;
        DateTime horaFinal = horaInicio.AddSeconds(ObterDuracao());
        string itemAtual = "";

        while (DateTime.Now < horaFinal)
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(itemAtual))
                    {
                        lock (_travamento)
                        {
                            _listaDoUsuario.Add(itemAtual.Trim());
                        }
                        Console.WriteLine();
                        itemAtual = "";
                    }
                }
                else if (tecla.Key == ConsoleKey.Backspace && itemAtual.Length > 0)
                {
                    itemAtual = itemAtual[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(tecla.KeyChar))
                {
                    itemAtual += tecla.KeyChar;
                    Console.Write(tecla.KeyChar);
                }
            }

            ExibirBarraDeTempo(horaInicio, horaFinal);
            lock (_travamento)
            {
                Console.Write($"  Itens: {_listaDoUsuario.Count}");
            }
            Thread.Sleep(100);
        }
        Console.WriteLine();

        int quantidade;
        lock (_travamento)
        {
            if (!string.IsNullOrWhiteSpace(itemAtual))
            {
                _listaDoUsuario.Add(itemAtual.Trim());
            }
            quantidade = _listaDoUsuario.Count;
        }

        Console.WriteLine($"Voce escreveu {quantidade} item(ns).");
        ExibirMensagemFinal();
    }

    public string ObterPerguntaAleatoria()
    {
        int indice = _aleatorio.Next(_itens.Count);
        return _itens[indice];
    }

    public List<string> ObterListaDoUsuario()
    {
        lock (_travamento)
        {
            return new List<string>(_listaDoUsuario);
        }
    }
}
