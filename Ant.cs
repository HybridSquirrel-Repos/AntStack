namespace AntStackV2
{
    public class Ant
    {
        private string _name;
        private int _legs;
        private int _id;

        public void SetName(string antName)
        {
            _name = antName;
        }

        public void SetLegs(int antLegs)
        {
            _legs = antLegs;
        }

        public void SetID(int antID)
        {
            _id = antID;
        }



        public string GetName()
        {
            return _name;
        }

        public int GetLegs()
        {
            return _legs;
        }

        public int GetID()
        {
            return _id;
        }
    }
}