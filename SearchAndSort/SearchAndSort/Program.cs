using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort
{
    public class Document : IComparable<Document>, IComparer<Document>
    {
        public Document(Number number, string nameOfDocument, State state, string fioOfResponsiblePerson, DateTime dateOfCreation)
        {
            Number = number;
            NameOfDocument = nameOfDocument;
            State = state;
            FIOOfResponsiblePerson = fioOfResponsiblePerson;
            DateOfCreation = dateOfCreation;
        }
        public Number Number { get; }
        public string NameOfDocument { get; }
        public State State { get; }
        public string FIOOfResponsiblePerson { get; }
        public DateTime DateOfCreation { get; }

        public int CompareTo(Document document)
        {
            if (document != null)
            {
                if (this == null)
                {
                    return -1;
                }
                return this.NameOfDocument.CompareTo(document.NameOfDocument);
            }
            else
            {
                if (this == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int Compare(Document document0, Document document1)
        {
            if (document0.DateOfCreation != document1.DateOfCreation)
            {
                if (document0.DateOfCreation > document1.DateOfCreation)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return document0.FIOOfResponsiblePerson.CompareTo(document1.FIOOfResponsiblePerson);
            }
        }

        public int MyCompare(Document document0, Document document1)
        {
            if (document0.Number.Num < document1.Number.Num)
            {
                return -1;
            }
            else if (document0.Number.Num > document1.Number.Num)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            Number number = obj as Number;
            if ((this.Number.Cipher == number.Cipher) && (this.Number.Num == number.Num) && (this.Number.Date == number.Date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            if (this != null)
            {
                return base.GetHashCode();
            }
            else
            {
                throw new Exception("Do exception");
            }
        }

        public void PrintDocument(Document doc)
        {
            Console.WriteLine("Number: cipher:" + doc.Number.Cipher + "; number: " + doc.Number.Num + "; date: " + doc.Number.Date + ";");
            Console.WriteLine("Name of document: " + doc.NameOfDocument + "; State: " + doc.State + "; FIO of responsible person: " + doc.FIOOfResponsiblePerson + "; Date of creation: " + doc.DateOfCreation + ";");
        }
    }
    public class Number
    {
        public Number(string cipher, int number, string date)
        {
            Cipher = cipher;
            Num = number;
            Date = date;
        }
        public string Cipher { get; set; }
        public int Num { get; set; }
        public string Date { get; set; }
    }
    public enum State
    {
        project,
        approved,
        finalized,
        isBeingApproved,
    }

    public class DocumentAgreagtor : IEqualityComparer<Document>
    {
        private List<Document> _documents;
        private Dictionary<Number, Document> _dictionaryOfDocumentsByNumber;
        private Dictionary<string, Document> _dictionaryOfDocumentsByFIOOfResponsiblePerson = new Dictionary<string, Document>();

        public DocumentAgreagtor(ICollection<Document> documents)
        {
            _documents = new List<Document>(documents);
        }

        public void Add(Document document)
        {
            _documents.Add(document);
        }

        public List<Document> GetAll()
        {
            return _documents;
        }

        public List<Document> GetAllOrdered()
        {
            List<Document> documentsOrdered = _documents;
            documentsOrdered.Sort();
            return documentsOrdered;
        }

        public List<Document> GetAllOrderedByDateOfCreation(Document doc)
        {
            List<Document> documentsOrdered = _documents;
            documentsOrdered.Sort(doc);
            return documentsOrdered;
        }

        public List<Document> GetAllOrderedByMySort(Document doc)
        {
            List<Document> documentsOrdered = _documents;
            documentsOrdered.Sort(doc.MyCompare);
            return documentsOrdered;
        }

        public Dictionary<Number, List<Document>> SearchByFullNumber(Number key)
        {
            Dictionary<Number, List<Document>> result = new Dictionary<Number, List<Document>>();
            List<Document> foundDocuments = new List<Document>();
            foreach (Document document in _documents)
            {
                if (Equals(document, key))
                {
                    foundDocuments.Add(document);
                }
            }
            result.Add(key, foundDocuments);
            return result;
        }
        public Dictionary<Number, List<Document>> SearchByCipherAndNumber(Number key)
        {
            Dictionary<Number, List<Document>> result = new Dictionary<Number, List<Document>>();
            List<Document> foundDocuments = new List<Document>();
            foreach (Document document in _documents)
            {
                if ((document.Number.Cipher == key.Cipher) && (document.Number.Num == key.Num))
                {
                    foundDocuments.Add(document);
                }
            }
            result.Add(key, foundDocuments);
            return result;
        }

        public Dictionary<string, List<Document>> SearchByFIOOfResponsiblePerson(string key)
        {
            Dictionary<string, List<Document>> result = new Dictionary<string, List<Document>>();
            List<Document> foundDocuments = new List<Document>();
            foreach (Document document in _documents)
            {
                if ((document.FIOOfResponsiblePerson == key) && (document.FIOOfResponsiblePerson == key))
                {
                    foundDocuments.Add(document);
                }
            }
            result.Add(key, foundDocuments);
            return result;
        }

        public bool Equals(Document doc1, Document doc2)
        {
            if ((doc1.Number.Cipher == doc2.Number.Cipher) && (doc1.Number.Num == doc2.Number.Num) && ((doc1.Number.Date == doc2.Number.Date)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode(Document document)
        {
            if (this != null)
            {
                return base.GetHashCode();
            }
            else
            {
                throw new Exception("Do exception");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = new DateTime(1999, 7, 29);
            DateTime dateTime1 = new DateTime(1998, 3, 24);
            DateTime dateTime2 = new DateTime(1999, 1, 9);
            DateTime dateTime3 = new DateTime(1989, 1, 29);
            DateTime dateTime4 = new DateTime(1999, 12, 12);
            DateTime dateTime5 = new DateTime(2021, 6, 2);

            Number number0 = new Number("0", 0, "1999.1.1");
            Number number1 = new Number("1", 1, "1999.2.2");
            Number number2 = new Number("2", 4, "1989.3.1");
            Number number3 = new Number("1", 1, "1991.1.4");
            Number number4 = new Number("4", 3, "1999.3.7");
            Number number5 = new Number("5", 2, "1996.4.1");
            Number number6 = new Number("6", 34, "1981.7.1");

            Number number7 = new Number("PD", 34, "2019.1.30");

            Document document0 = new Document(number0, "B", State.isBeingApproved, "Smith", dateTime);
            Document document1 = new Document(number1, "C", State.finalized, "Belkin", dateTime1);
            Document document2 = new Document(number2, "A", State.isBeingApproved, "Alexandrov", dateTime3);
            Document document3 = new Document(number3, "Z", State.project, "Nikolaev", dateTime2);
            Document document4 = new Document(number4, "L", State.isBeingApproved, "Alexin", dateTime5);
            Document document5 = new Document(number5, "S", State.approved, "Orlov", dateTime4);
            Document document6 = new Document(number6, "N", State.isBeingApproved, "Petrov", dateTime3);

            Document document7 = new Document(number7, "Programma test", State.isBeingApproved, "Smith", dateTime5);

            List<Document> documents = new List<Document>
            {
                document0,
                document1,
                document2,
                document3,
                document4,
                document5,
                document6,
                document7
            };
            DocumentAgreagtor agreagtorList = new DocumentAgreagtor(documents);
            Console.WriteLine("Original order: ");
            foreach (Document doc in agreagtorList.GetAll())
            {
                doc.PrintDocument(doc);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Sort by name of document order: ");
            foreach (Document doc in agreagtorList.GetAllOrdered())
            {
                doc.PrintDocument(doc);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Sort by date of creation order: ");
            foreach (Document doc in agreagtorList.GetAllOrderedByDateOfCreation(document0))
            {
                doc.PrintDocument(doc);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Sort by number of document order: ");
            foreach (Document doc in agreagtorList.GetAllOrderedByMySort(document0))
            {
                doc.PrintDocument(doc);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Search by full number: ");
            Console.WriteLine("Enter cipher: ");
            string searchCipher = Console.ReadLine();
            Console.WriteLine("Enter num: ");
            int searchNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter date: ");
            string searchDate = Console.ReadLine();

            Number keyNumber = new Number(searchCipher, searchNum, searchDate);

            var searchByFullNumber = agreagtorList.SearchByFullNumber(keyNumber);
            foreach (var searchDocs in searchByFullNumber)
            {
                foreach (var searchDoc in searchDocs.Value)
                {
                    searchDoc.PrintDocument(searchDoc);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Search by cipher and num: ");
            Console.WriteLine("Enter cipher: ");
            searchCipher = Console.ReadLine();
            Console.WriteLine("Enter num: ");
            searchNum = Convert.ToInt32(Console.ReadLine());

            Number keyNumber1 = new Number(searchCipher, searchNum, number0.Date);

            var searchByCipherAndNumber = agreagtorList.SearchByCipherAndNumber(keyNumber1);
            foreach (var searchDocs in searchByCipherAndNumber)
            {
                foreach (var searchDoc in searchDocs.Value)
                {
                    searchDoc.PrintDocument(searchDoc);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Search by fio of responsible person: ");
            Console.WriteLine("Enter fio of responsible person: ");
            string searchFIO = Console.ReadLine();

            var searchByFIO = agreagtorList.SearchByFIOOfResponsiblePerson(searchFIO);
            foreach (var searchDocs in searchByFIO)
            {
                foreach (var searchDoc in searchDocs.Value)
                {
                    searchDoc.PrintDocument(searchDoc);
                }
            }

            Console.ReadLine();
        }
    }
}
