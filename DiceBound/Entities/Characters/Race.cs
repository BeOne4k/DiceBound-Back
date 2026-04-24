    using DiceBound.Common;

    namespace DiceBound.Entity_s.Characters
    {
        public class Race : BaseEntity
        {
            public string Name { get; set; } = null!;

            public int BaseStrength { get; set; }
            public int BaseDexterity { get; set; }
            public int BaseConstitution { get; set; }
            public int BaseIntelligence { get; set; }

            public ICollection<Character> Characters { get; set; } = new List<Character>();
        }

    }
