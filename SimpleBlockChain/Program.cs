using System;

namespace SimpleBlockChain
{
    class Program
    {
        static void Main(string[] args)
        {
            const string minerAddress = "miner1";
            const string user1Address = "A";
            const string user2Address = "B";

            BlockChain blockChain = new BlockChain(proofOfDifficultyOfWork:3,miningreward:10);
            blockChain.CreateTransaction(new Transactions(user1Address,user2Address,200));
            blockChain.CreateTransaction(new Transactions(user2Address, user1Address, 10));

            Console.WriteLine($"Chain is valid: {blockChain.IsValidChain()}");
            Console.WriteLine("\n" + "---------Started mining---------");
            blockChain.MineBlock(minerAddress);

            Console.WriteLine($"Balance of the miner is: {blockChain.GetBalance(minerAddress)}");

            blockChain.CreateTransaction(new Transactions(user1Address,user2Address,5));
            Console.WriteLine("\n" + "---------Started mining---------");
            blockChain.MineBlock(minerAddress);
            Console.WriteLine($"Balance of the miner is: {blockChain.GetBalance(minerAddress)}");
            Console.WriteLine();
            PrintChain(blockChain);

            Console.ReadKey();
        }

        public static void PrintChain(BlockChain blockChain)
        {
            Console.WriteLine("----------------- Start Blockchain -----------------");
            foreach (Block block in blockChain.Chain)
            {
                Console.WriteLine();
                Console.WriteLine("------ Start Block ------");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine("--- Start Transactions ---");
                foreach (Transactions transaction in block.TransactionsList)
                {
                    Console.WriteLine("From: {0} To {1} Amount {2}", transaction.From, transaction.To, transaction.Amount);
                }
                Console.WriteLine("--- End Transactions ---");
                Console.WriteLine("------ End Block ------");
            }
            Console.WriteLine("----------------- End Blockchain -----------------");
        }
    }
}
