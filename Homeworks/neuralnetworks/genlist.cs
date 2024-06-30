// A simple class consisting of a generic list (holds any type), which is able to grow, e.g. it is possible to add an item

public class genlist<T>{
	public T[] data; // Generating an array of any type called "data"
	public int size => data.Length; // Size takes less time to wrtie than data.Length
	public T this[int i] => data[i]; // Indexer used to access instances within the array "data"
	public genlist(){ data = new T[0]; } // Constructor creating an empty array of type T
	public void add(T item){ // Method "add" taking parameter "item" of type T
		T[] newdata = new T[size + 1]; // Generating array "newdata" 1 longer than array "data"
		System.Array.Copy(data, newdata, size); // "Size" number of elements copied from data to newdata
		newdata[size] = item; // Adding item to the newdata array
		data = newdata; // Go back to original name 
	}
}
