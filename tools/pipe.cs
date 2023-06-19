using System;

public class pipe {
  public static (genlist<double>, genlist<double>) twocols() {
    genlist<double> a = new genlist<double>();
    genlist<double> b = new genlist<double>();

    char[] delimiterChars = {' ', ',', ':', '\t'};

    string s;
    while ((s = Console.ReadLine()) != null) {
      string[] splits = s.Split(delimiterChars);
      
      a.add(Convert.ToDouble(splits[0]));
      b.add(Convert.ToDouble(splits[1]));
    }

    return (a, b);
  }

  public static (genlist<double>, genlist<double>, genlist<double>) threecols() {
    genlist<double> a = new genlist<double>();
    genlist<double> b = new genlist<double>();
    genlist<double> c = new genlist<double>();

    char[] delimiterChars = {' ', ',', ':', '\t'};

    string s;
    while ((s = Console.ReadLine()) != null) {
      string[] splits = s.Split(delimiterChars);
      
      a.add(Convert.ToDouble(splits[0]));
      b.add(Convert.ToDouble(splits[1]));
      c.add(Convert.ToDouble(splits[1]));
    }

    return (a, b, c);
  }
}

public class genlist<T> {
  public T[] data;
  public int size => data.Length;
  public T this[int i] => data[i];
  public genlist() {data = new T[0];}
  public void add(T item) {
    T[] newdata = new T[size + 1];
    System.Array.Copy(data, newdata, size);
    newdata[size]=item;
    data=newdata;
  }
}
