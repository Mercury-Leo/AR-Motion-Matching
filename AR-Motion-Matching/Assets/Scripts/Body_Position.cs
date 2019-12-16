using System;
using System.Runtime.Serialization;
using UnityEngine;

public class Body_Position : MonoBehaviour
{
    public Body_Position(DateTime time, Vector3 head_vec, Vector3 neck_vec, Vector3 leftarm_vec, Vector3 rightarm_vec)
    {
        Time = time;
        head = new Head(head_vec);
        neck = new Neck(neck_vec);
        leftarm = new LeftArm(leftarm_vec);
        rightarm = new RightArm(rightarm_vec);
    }

    [DataMember]
	public DateTime Time { get; set; }
	[DataMember]
	public Head head { get; set; }
	[DataMember]
	public Neck neck { get; set; }
    [DataMember]
    public LeftArm leftarm { get; set; }
    [DataMember]
    public RightArm rightarm { get; set; }


    [DataContract]
	public class Head
	{

		public Head(Vector3 pos)
		{
			Head_X_cords = pos.x;
			Head_Y_cords = pos.y;
			Head_Z_cords = pos.z;
		}

		[DataMember]
		public float Head_X_cords { get; set; }
		[DataMember]
		public float Head_Y_cords { get; set; }
		[DataMember]
		public float Head_Z_cords { get; set; }

	}

	[DataContract]
	public class Neck
	{

		public Neck(Vector3 pos)
		{
			Neck_X_cords = pos.x;
			Neck_Y_cords = pos.y;
			Neck_Z_cords = pos.z;
		}
		[DataMember]
		public float Neck_X_cords { get; set; }
		[DataMember]
		public float Neck_Y_cords { get; set; }
		[DataMember]
		public float Neck_Z_cords { get; set; }
	}

    [DataContract]
    public class LeftArm
    {

        public LeftArm(Vector3 pos)
        {
            LeftArm_X_cords = pos.x;
            LeftArm_Y_cords = pos.y;
            LeftArm_Z_cords = pos.z;
        }
        [DataMember]
        public float LeftArm_X_cords { get; set; }
        [DataMember]
        public float LeftArm_Y_cords { get; set; }
        [DataMember]
        public float LeftArm_Z_cords { get; set; }
    }

    [DataContract]
    public class RightArm
    {

        public RightArm(Vector3 pos)
        {
            RightArm_X_cords = pos.x;
            RightArm_Y_cords = pos.y;
            RightArm_Z_cords = pos.z;
        }
        [DataMember]
        public float RightArm_X_cords { get; set; }
        [DataMember]
        public float RightArm_Y_cords { get; set; }
        [DataMember]
        public float RightArm_Z_cords { get; set; }
    }

    public static Body_Position CreateComponent(GameObject where, DateTime time, Vector3 Head_pos, Vector3 Neck_pos, Vector3 LeftArm_pos, Vector3 RightArm_pos)
    {
        Body_Position body = where.AddComponent<Body_Position>();
        body.Time = time;
        body.head = new Body_Position.Head(Head_pos);
        body.neck = new Body_Position.Neck(Neck_pos);
        body.leftarm = new Body_Position.LeftArm(LeftArm_pos);
        body.rightarm = new Body_Position.RightArm(RightArm_pos);
        return body;
    }

}
