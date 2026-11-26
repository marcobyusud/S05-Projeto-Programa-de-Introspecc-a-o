namespace Introspeccao;

public class AtividadeDeRespiracao : Atividade
{
    public AtividadeDeRespiracao()
        : base(
            "Atividade de Respiracao",
            "Esta atividade ajudara voce a relaxar, inspirando e expirando lentamente. Limpe sua mente e concentre-se na sua respiracao.")
    {
    }

    public override void Executar()
    {
        ExibirMensagemInicial();

        DateTime horaFinal = DateTime.Now.AddSeconds(ObterDuracao());
        bool inspirando = true;

        while (DateTime.Now < horaFinal)
        {
            int segundosRestantes = (int)Math.Ceiling((horaFinal - DateTime.Now).TotalSeconds);
            int segundosDaMensagem = Math.Min(5, segundosRestantes);

            if (inspirando)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n  ~ INSPIRE ~");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n  ~ EXPIRE ~");
            }
            Console.ResetColor();

            ExibirCicloDeRespiracao(inspirando, segundosDaMensagem);
            inspirando = !inspirando;
        }
        Console.WriteLine();

        ExibirMensagemFinal();
    }

    private void ExibirCicloDeRespiracao(bool inspirando, int segundos)
    {
        DateTime inicio = DateTime.Now;

        for (int numero = 1; numero <= segundos; numero++)
        {
            if (inspirando)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\r  ({new string('o', Math.Min(numero, 8))}) Inspirando...");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"\r  ({new string('o', Math.Max(8 - numero, 1))}) Expirando...");
            }
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        Console.Write("\r" + new string(' ', 40) + "\r");
    }
}
