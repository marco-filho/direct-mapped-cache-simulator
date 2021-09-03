using System;
using System.IO;
using Memory;

namespace av1_ac
{
    class Simulator
    {
        static Ram ram;
        static Cache cache;

        static void Main(string[] args)
        {
            Console.WriteLine("### Simulador de Cache com Mapeamento Direto ###");
            Console.WriteLine("* Recomenda-se utilizar poucos bits para melhor visualizar a execução do programa! *");

            Console.WriteLine("Qual o tamanho da Memória Principal?");
            int inputRam = ValidateInput(Console.ReadLine());

            Console.WriteLine("Qual o tamanho da Memória Cache?");
            int inputCache = ValidateInput(Console.ReadLine());

            GenerateMemory(inputRam, inputCache);

            Accesser.Access(cache, 100);

            File.WriteAllText("Resultado.txt", Report());
        }

        static void GenerateMemory(int ramSize, int cacheSize) {
            ram = new Ram(ramSize);
            cache = new Cache(ram, cacheSize);
        }

        static int ValidateInput(string input) {
            int validatedInput;
            
            try {
                validatedInput = int.Parse(input);
                if (validatedInput < 0 || MathF.Log2(validatedInput) % 1 != 0)
                    throw new Exception();
            }
            catch (Exception) {
                throw new Exception("Entrada inválida!\n" +
                    "Deve ser um número inteiro maior que zero e ser potência de base 2.");
            }

            return validatedInput;
        }

        static string Report() {
            string mainMemoryAdresses = "", cacheLines = "", instructions = "";

            foreach(string a in ram.Addresses) {
                mainMemoryAdresses += "\t" + a + ",\n";
            }
            foreach(Line l in cache.Lines) {
                cacheLines += "\t" + l + ",\n";
            }
            foreach(string a in Accesser.Instructions) {
                instructions += "\t" + a + ",\n";
            }

            return (
                "#### RESULTADO ####\n" +
                "\n## Memoria\n" +
                "# Endereços da Memória Principal: [\n" +
                mainMemoryAdresses +
                "]\n" +
                "# Linhas da Cache: [\n" +
                cacheLines +
                "]\n" +
                "\n## Acesso\n" +
                "# Instruções: [\n" +
                instructions + 
                "]" +
                "\n# Acertos: " + Accesser.Hits +
                "\nTaxa (HitRate): " + Accesser.HitRate +
                "\n# Erros (Miss): " + Accesser.Misses +
                "\nTaxa (MissRate): " + Accesser.MissRate
            );
        }
    }
}
