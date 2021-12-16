using Lib;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe16A : AufgabeAbstract {

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
			this.result = VersionSum(root);
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

		int VersionSum(BinPacket packet) {
			int sum = packet.Version;
			if(packet.ID != 4) {
				foreach(var sub in packet.Subs) {
					sum += VersionSum(sub);
				}
			}
			return sum;
		}

	}
}