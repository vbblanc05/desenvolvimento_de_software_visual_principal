int tamanho = 100;
int[] vetor = new int[tamanho];

Random random = new Random();
for (int i = 0; i < tamanho; i++)
{
    vetor[i] = random.Next(1000);
}

for (int i = 0; i < tamanho; i++)
{
    Console.Write(vetor[i] + " ");
}

bool troca = false;
do
{
    troca = false;
    for (int i = 0; i < vetor.Length - 1; i++)
    {
        int atual = vetor[i];
        int proximo = vetor[i + 1];
        if (atual > proximo)
        {
            troca = true;
            int aux = atual;
            vetor[i] = proximo;
            vetor[i + 1] = aux;
        }
    }
} while (troca == true);

Console.WriteLine("\n");
for (int i = 0; i < tamanho; i++)
{
    Console.Write(vetor[i] + " ");
}