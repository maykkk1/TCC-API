namespace Gerenciador.Domain.Enums;

public enum DificuldadeTarefaEnum
{
    Facil = 0,
    Normal = 1,
    Dificil = 2,
    Lendaria = 3
}

public class DificuldadeTarefa
{
    public static int ObterPontos(DificuldadeTarefaEnum dificuldade)
    {
        switch (dificuldade)
        {
            case DificuldadeTarefaEnum.Facil:
                return 40;
            case DificuldadeTarefaEnum.Normal:
                return 60;
            case DificuldadeTarefaEnum.Dificil:
                return 80;
            case DificuldadeTarefaEnum.Lendaria:
                return 100;
            default:
                throw new ArgumentOutOfRangeException(nameof(dificuldade), dificuldade, null);
        }
    }
}