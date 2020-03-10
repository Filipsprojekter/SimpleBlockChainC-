using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBlockChain
{
    class BlockChain
    {
        private readonly int _proofOfDifficultyOfWork;
        private readonly double _miningReward;

        private List<Transactions> _pendingTransactions;

        public List<Block> Chain { get; set; }

        public BlockChain(int proofOfDifficultyOfWork, double miningreward)
        {
            _proofOfDifficultyOfWork = proofOfDifficultyOfWork;
            _miningReward = miningreward;

            _pendingTransactions = new List<Transactions>();
            Chain = new List<Block>() {CreateGenesisBlock()};
        }

        public void CreateTransaction(Transactions transaction)
        {
            _pendingTransactions.Add(transaction);
        }

        public void MineBlock(string minerAddress)
        {
            Transactions minerRewardTransaction = new Transactions(null,minerAddress,_miningReward);
            _pendingTransactions.Add(minerRewardTransaction);
            Block block = new Block(DateTime.Now,_pendingTransactions);
            block.MineBlock(_proofOfDifficultyOfWork);

            block.PreviousHash = Chain.Last().Hash;
            Chain.Add(block);

            _pendingTransactions = new List<Transactions>();
        }

        public bool IsValidChain()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block previousBlock = Chain[i - 1];
                Block currentBlock = Chain[i];

                if (currentBlock.Hash != currentBlock.CreateHash()) return false;
                if (currentBlock.PreviousHash != previousBlock.PreviousHash) return false;
            }

            return true;
        }

        public double GetBalance(string address)
        {
            double balance = 0;

            foreach (var block in Chain)
            {
                foreach (var transaction in block.TransactionsList)
                {
                    if (transaction.From == address) balance -= transaction.Amount;
                    if (transaction.To == address) balance += transaction.Amount;
                }

            }

            return balance;
        }
        private Block CreateGenesisBlock()
        {
            List<Transactions> firstTransaction = new List<Transactions>() {new Transactions("","",0)};
            return new Block(DateTime.Now,firstTransaction,"0");
        }
    }
}
