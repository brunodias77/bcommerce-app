using Bogus;

namespace bcommerce_server.UnitTest.Common;

public abstract class BaseFixture
{
    /// <summary>
    /// Instância do Faker usada para gerar dados fictícios em português do Brasil.
    /// </summary>
    public Faker Faker { get; set; }

    /// <summary>
    /// Construtor protegido que inicializa o Faker com a localidade "pt_BR".
    /// </summary>
    protected BaseFixture()
        => Faker = new Faker("pt_BR");

    /// <summary>
    /// Gera um valor booleano aleatório.
    /// Retorna verdadeiro aproximadamente 50% das vezes e falso nas demais.
    /// </summary>
    /// <returns>Um valor booleano aleatório.</returns>
    public bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
}