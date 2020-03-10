using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SimpleBlockChain
{
    class Block
    {
        // nonce, hash, previoushash, timestamp, data

        private readonly DateTime _timeStamp;
        private long _nonce;

        public string PreviousHash { get; set; }
        public List<Transactions> TransactionsList { get; set; }
        public string Hash { get;private set; }

        public Block(DateTime timeStamp, List<Transactions> transactionsList, string previousHash = "" )
        {
            _timeStamp = timeStamp;
            _nonce = 0;
            TransactionsList = transactionsList;
            PreviousHash = previousHash;
            Hash = CreateHash();
        }


        public string CreateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = PreviousHash + _timeStamp + _nonce + TransactionsList;

                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Encoding.UTF8.GetString(bytes);
            }
        }

        public void MineBlock(int proveOfDifficultyOfWork)
        {
            string hashValidationTemplate = new String('0',proveOfDifficultyOfWork);

            while (Hash.Substring(0, proveOfDifficultyOfWork) != hashValidationTemplate)
            {
                _nonce++;
                Hash = CreateHash();
            }

            Console.WriteLine($"Block with Hash = {Hash} mined");
        }
    }   
}
