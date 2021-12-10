int x = 0;
int y = 0;
int sw = 10;


void setup()
{
  pinMode(sw, INPUT_PULLUP);
  Serial.begin(9600);
}
void loop()
{
  if (true)
  {
    x = analogRead(A0);
    y = analogRead(A1);

    int valueSW = digitalRead(sw);



    if (valueSW != 0) {
      int a = floor(map(x, 0, 1024, -1, 2));
      Serial.println(a);
      
    }else {
       Serial.println(2);
       delay(100);  
      }

  }
  delay(10);
}
