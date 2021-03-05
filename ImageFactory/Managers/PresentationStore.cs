﻿using ImageFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageFactory.Managers
{
    // This is probably some of the shadiest code I've written in a while ngl (PART 1)
    // Use any other part of this mod as an example but this
    internal class PresentationStore
    {
        private readonly IEnumerable<float> _oneToOneHundred;
        private readonly IEnumerable<Value> _presentationValues;

        public PresentationStore()
        {
            //_oneToOneHundred = Enumerable.Range(5, 95).Select(f => Math.Round(f * 0.01, 2)));
            _oneToOneHundred = Enumerable.Range(5, 95).Select(f => (float)decimal.ToDouble((decimal)Math.Round(f * 0.01d, 2)));
            //_oneToOneHundred = new float[] { 0.5f, 0.51f, 0.52f, 0.53f, 0.54f, 0.55f, 0.56f, 0.57f, 0.58f, 0.59f, 0.60f, 0.61f, 0.62f, 0.63f, 0.64f, 0.65f, 0.66f, 0.67f, 0.68f }
            var xcast = Enumerable.Range(1, 95).Select(i => (object)(i * 10)).ToList();
            var casted = _oneToOneHundred.Cast<object>().ToList();

            _presentationValues = new List<Value>
            {
                new Value("Everywhere"),
                new Value("In Menu"),
                new Value("Results Screen", false, new ValueConstructor("When", "Finished", new List<object> { "Finished", "Passed", "Failed" })),
                new Value("In Song"),
                new Value("%", false, new ValueConstructor("When", "Below", new List<object> { "Below", "Above" }), new ValueConstructor("%", 0.8f, casted)),
                new Value("% Range", false, new ValueConstructor("When Above (%)", 0.8f, casted), new ValueConstructor("and Below (%)", 0.9f, casted)),
                new Value("Combo", true, new ValueConstructor("On Combo", 100, xcast)),
                new Value("Combo Increment", true, new ValueConstructor("On Every X Combo", 100, xcast)),
                new Value("Combo Drop", true),
                new Value("On Last Note", true)
            };
        }

        public IEnumerable<Value> Values()
        {
            return _presentationValues;
        }

        public class Value
        {
            public string ID { get; }
            public bool HasDuration { get; }
            public IEnumerable<ValueConstructor> Constructors { get; }

            public Value(string id, bool hasDuration = false)
            {
                ID = id;
                HasDuration = hasDuration;
                Constructors = Array.Empty<ValueConstructor>();
            }

            public Value(string id, bool hasDuration, ValueConstructor constructor)
            {
                ID = id;
                HasDuration = hasDuration;
                Constructors = new ValueConstructor[] { constructor };
            }

            public Value(string id, bool hasDuration, params ValueConstructor[] constructors)
            {
                ID = id;
                HasDuration = hasDuration;
                Constructors = constructors;
            }
        }
    }
}