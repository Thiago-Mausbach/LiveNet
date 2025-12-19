namespace LiveNet.Infrastructure;

public static class EntityDiffValidate
{
    /// <summary>
    /// Compara as propriedades do objeto salvo no banco com sua versão atualizada, atualizando as props que foram alteradas
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original">Objeto já salvo no banco, usado como base de comparação</param>
    /// <param name="novo">Objeto contendo as atualizações</param>
    public static void ValidarDif<T>(T original, T novo)
    {
        var props = typeof(T).GetProperties();

        foreach (var prop in props)
        {
            if (!prop.CanWrite) continue;
            if (prop.Name == "Id") continue;

            var valorOriginal = prop.GetValue(original);
            var novoValor = prop.GetValue(novo);

            if (!Equals(valorOriginal, novoValor))
                prop.SetValue(original, novoValor);
        }
    }
}