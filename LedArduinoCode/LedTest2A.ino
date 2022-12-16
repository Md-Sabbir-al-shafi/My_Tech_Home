int led = 11;
int getCommand;
void setup() {
  Serial.begin(9600);
  pinMode(led, OUTPUT);
}

void loop() {

  while (Serial.available() != 0)
  {
    getCommand = Serial.parseInt();

    Serial.read();
    if (getCommand == 256)
    {
      digitalWrite(led, LOW);
    }
    else if (getCommand == 257)
    {
      digitalWrite(led, HIGH);
    }
    else {
      analogWrite(led, getCommand);
    }

  }

}
/*Serial.available( )= The Serial.available( ) function in Arduino gets the stored bytes from the serial port that are available for 
 * reading. It is the data,which is already stored and arrived in the serial buffer. The serial buffer in Arduino holds the 64 bytes.
 Serial.available( ) function inherits from the utility class called stream. The stream is only invoked when the function relying on 
 it is called. The stream class is considered as the base class for binary and character-based streams.Get the number of bytes 
 (characters) available for reading from the serial port. This is data that’s already arrived and stored in the serial receive buffer 
 (which holds 64 bytes). See the list of available serial ports for each board on the Serial main page.return The number of bytes available to
 read .*/
 /*Stream=Stream is the base class for character and binary based streams. It is not called directly, but invoked whenever you use a function that relies on it.
Stream defines the reading functions in Arduino. When using any core functionality that uses a read() or similar method, you can safely assume it calls on the 
Stream class. For functions like print(), Stream inherits from the Print class.*/
/*Serial=Used for communication between the Arduino board and a computer or other devices. All Arduino boards have at least one serial 
port (also known as a UART or USART), and some have several.*/

/*Serial.read()=Reads incoming serial data.Serial.read() inherits from the Stream utility class.See the list of available serial ports 
for each board on the Serial main page.*/

/*Serial.parseInt()= The parseInt() function from the Serial library is made to scan down the serial receive buffer one byte at a time in 
 * search of the first valid numerical digit.So if you have “314” in the serial receive buffer, you’d get 314 returned the first time you 
 * call Serial.parseInt().If you had “I ate 314tacos” in the serial receive buffer, you’d only get 314 returned the first time you call 
 * Serial.parseInt().What does Serial.parseInt do with the non-numeric values in the serial receive buffer? If the non-numeric values are 
 * only BEFORE a valid integer, it tosses them out and returns the integer and leaves the rest.What does Serial.parseInt() do if there 
 * are only non-numeric numbers in the serial receive buffer? Like “¡Arriba, arriba!”? If all parseInt() can see in the serial receive 
 * buffer are non-numeric values, it will return a zero, and leave the values sitting in the buffer.If you start get a bunch of 0’s 
 * returned from parseInt() and you’re not sure why, remember that Newlines (NL) and Carriage Returns (CR) may be added when using the 
 * Serial Monitor window in the Arduino IDE (even though you won’t see them in the Send section where you enter the text).  If you don’t 
 * want these terminating characters, make sure to select No Line Ending from the drop down.A common method of using Serial.parseInt() 
 *is to pair it with a while loop and Serial.available(), so that the only time you check for a new integer is when data has actually 
 *arrived at the serial port. (If this code structure looks odd to you, then check out the lesson we did on Serial.read() to help 
 *explain it.)*/
