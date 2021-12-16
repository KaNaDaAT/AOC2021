using Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe16B : AufgabeAbstract {

		private class BinPacket {
			public int Version;
			public int ID;
			public long Value;
			public List<BinPacket> Subs;
		}

		protected override void Runner(Reader reader) {
			BitArray bits = Utils.ConvertHexToBitArray(reader.ReadAll());
			int start = 0;
			BinPacket root = GetRootPacket(ref bits, ref start, bits.Length - 1);
			this.result = Calculate(root);
		}

		BinPacket GetRootPacket(ref BitArray bits, ref int start, int end) {

			var packet = new BinPacket();
			packet.Version = (int) NumberInRange(ref bits, ref start, 3);
			packet.ID = (int) NumberInRange(ref bits, ref start, 3);
			switch(packet.ID) {
				case 4:
					bool isLast = false;
					while(!isLast) {
						isLast = !bits.Get(start);
						start++;
						packet.Value *= 16L;
						packet.Value += NumberInRange(ref bits, ref start, 4);
					}
					break;
				default:
					// Operator
					packet.Subs = new List<BinPacket>();
					var isLength11 = bits.Get(start);
					start++;
					if(isLength11) {
						long length = NumberInRange(ref bits, ref start, 11);
						while(packet.Subs.Count < length) {
							BinPacket subPacket = GetRootPacket(ref bits, ref start, end);
							packet.Subs.Add(subPacket);
						}
					} else {
						long length = NumberInRange(ref bits, ref start, 15);
						long subPacketEnd = start + length;
						while(start < subPacketEnd) {
							BinPacket subPacket = GetRootPacket(ref bits, ref start, end);
							packet.Subs.Add(subPacket);
						}
					}
					break;
			}
			return packet;
		}


		long NumberInRange(ref BitArray bits, ref int start, int count) {
			long result = 0;
			int end = count + start;
			for(; start < end; start++) {
				result = result << 1;
				if(bits.Get(start))
					result = result | 1;
			}
			return result;
		}

		long Calculate(BinPacket packet) {
			if(packet.ID >= 5 && packet.ID <= 7) {
				long a = Calculate(packet.Subs[0]);
				long b = Calculate(packet.Subs[1]);
				return packet.ID switch {
					5 => a > b ? 1 : 0,
					6 => b > a ? 1 : 0,
					7 => a == b ? 1 : 0,
					_ => throw new Exception()
				};
			}
			return packet.ID switch {
				0 => packet.Subs.Sum(sub => Calculate(sub)),
				1 => packet.Subs.Aggregate(1L, (acc, sub) => acc * Calculate(sub)),
				2 => packet.Subs.Min(sub => Calculate(sub)),
				3 => packet.Subs.Max(sub => Calculate(sub)),
				4 => packet.Value,
				_ => throw new Exception()
			};
		}

	}
}