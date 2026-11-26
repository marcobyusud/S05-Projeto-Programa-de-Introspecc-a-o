namespace Introspeccao;

public class AtividadeDeReflexao : Atividade
{
    private readonly Random _aleatorio = new Random();
    private readonly List<string> _reflexoes = new List<string>
    {
        "Pense em uma ocasiao em que voce defendeu outra pessoa.",
        "Pense em uma ocasiao em que voce fez algo realmente dificil.",
        "Pense em uma ocasiao em que voce ajudou alguem necessitado.",
        "Pense em uma ocasiao em que voce fez algo verdadeiramente altruista."
    };

    private readonly List<string> _perguntas = new List<string>
    {
        "Por que essa experiencia foi significativa para voce?",
        "Voce ja fez algo assim antes?",
        "Como voce comecou?",
        "Como voce se sentiu quando terminou?",
        "O que tornou esse momento diferente de outras vezes em que voce nao teve tanto sucesso?",
        "Qual e a sua coisa favorita sobre essa experiencia?",
        "O que voce pode aprender com essa experiencia que se aplica a outras situacoes?",
        "O que voce aprendeu sobre si mesmo por meio dessa experiencia?",
        "Como voce pode manter essa experiencia em mente no futuro?"
    };

    public AtividadeDeReflexao()
        : base(
            "Atividade de Reflexao",
            "Esta atividade ajudara voce a refletir sobre momentos da sua vida em que voce demonstrou força e resiliencia. Isso ajudara voce a reconhecer o poder que voce tem e como pode usa-lo em outros aspectos da sua vida.")
    {
    }

    public override void Executar()
    {
        ExibirMensagemInicial();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  Tema da sua reflexao:");
        Console.ResetColor();
        Console.WriteLine(ObterItemAleatorioSemRepetir(_reflexoes));

        List<string> perguntasDisponiveis = new List<string>(_perguntas);
        DateTime horaInicio = DateTime.Now;
        DateTime horaFinal = horaInicio.AddSeconds(ObterDuracao());

        while (DateTime.Now < horaFinal)
        {
            if (perguntasDisponiveis.Count == 0)
            {
                perguntasDisponiveis = new List<string>(_perguntas);
            }

            int indice = _aleatorio.Next(perguntasDisponiveis.Count);
            string pergunta = perguntasDisponiveis[indice];
            perguntasDisponiveis.RemoveAt(indice);

            Console.WriteLine();
            Console.WriteLine(pergunta);

            int segundosRestantes = (int)Math.Ceiling((horaFinal - DateTime.Now).TotalSeconds);
            int segundos = Math.Min(5, segundosRestantes);
            if (segundos > 0)
            {
                ExibirMomentoDeReflexao(segundos, horaInicio, horaFinal);
            }
        }

        ExibirMensagemFinal();
    }

    private void ExibirMomentoDeReflexao(int segundos, DateTime inicio, DateTime fim)
    {
        for (int numero = segundos; numero > 0; numero--)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            ExibirBarraDeTempo(inicio, fim);
            Console.Write($"  Respirando com calma ({numero}s)");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        Console.Write("\r" + new string(' ', 80) + "\r");
    }

    private string ObterItemAleatorioSemRepetir(List<string> itens)
    {
        int indice = _aleatorio.Next(itens.Count);
        return itens[indice];
    }

    public string ObterReflexoesAleatoria()
    {
        return ObterItemAleatorioSemRepetir(_reflexoes);
    }

    public string ObterPerguntasAleatoria()
    {
        return ObterItemAleatorioSemRepetir(_perguntas);
    }

    public void ExibirReflexoes()
    {
        Console.WriteLine(ObterReflexoesAleatoria());
    }

    public void ExibirPerguntas()
    {
        Console.WriteLine(ObterPerguntasAleatoria());
    }
}
