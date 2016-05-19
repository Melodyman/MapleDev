namespace MapleDev83.Network.PacketHandling
{
    public class MaplePacketCreator
    {
        public byte[] loginError(ushort error)
        {
            PacketWriter writer = new PacketWriter((ushort)SendOpcodes.SendOpcode.LOGIN_STATUS);
            writer.WriteUShort(error);
            writer.WriteUInt(0);
            return writer.ToArray();
        }

        public byte[] loginSuccess(string username, bool admin, int accountid, bool female)
        {
            PacketWriter writer = new PacketWriter((ushort)SendOpcodes.SendOpcode.LOGIN_STATUS);
            writer.WriteInt(0);
            writer.WriteShort(0);
            writer.WriteInt(accountid);
            writer.WriteBool(female);
            writer.WriteBool(admin);
            if (admin)
                writer.WriteShort(0x40);
            else
                writer.WriteShort(0);
            writer.WriteSByte(0);
            writer.WriteSByte(0);
            writer.WriteMapleString(username);
            writer.WriteSByte(0);
            writer.WriteSByte(0); // muteReason?
            writer.WriteLong(0); // mute reset ?
            writer.WriteLong(0); // creation date?
            writer.WriteInt(0);
            writer.WriteUShort(2);
            writer.WriteShort(0);

            return writer.ToArray();
        }
    }
}
