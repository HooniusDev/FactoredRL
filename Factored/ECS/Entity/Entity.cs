namespace Factored.ECS
{
	public class Entity
	{
		public int ID { get; private set; }

		public Entity( int id )
		{
			ID = id;
		}
	}
}