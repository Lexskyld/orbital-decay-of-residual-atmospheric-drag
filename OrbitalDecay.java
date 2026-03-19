import java.util.ArrayList;
public class OrbitalDecay{
    public static double G = 6.67430e-11;
    public static double M_Earth = 5.972e24;
    public static double R_Earth = 6371000;

public static void main(String[]args){
StringBuilder csvContent = new StringBuilder("Day,Altitude\n");
    double M_Satelite = 5.00;
    double A_Satelite = 0.01;
    double C_Drag = 2.2;
    double Altitude = 400000;
    double R = R_Earth + Altitude;
    double time = 0;
    double DistanceTime = 1000;

System.out.println ("| Day |  Altitude (km)  |  Velocity (m/s)  |  Density (kg/m3)  |");
System.out.println ("-----------------------------------------------------------------");

while (Altitude > 150000){
    double rho = findDensity(Altitude);
    double v = Math.sqrt((G * M_Earth) / R);
    double dragForce = 0.5 * rho * v * v * C_Drag * A_Satelite;
    double deltaR = (2 * dragForce * v * DistanceTime) / (M_Satelite * (G * M_Earth / (R * R)));

    R -= deltaR;
    Altitude = R - R_Earth;
    time += DistanceTime;

    if (time % 86400 < DistanceTime){
        System.out.printf("| %-3.0f  | %-15.6f | %-16.2f | %-10.2e |\n",
                           time / 86400, Altitude / 1000, v , rho);
    csvContent.append(time/86400).append(",").append(Altitude/1000).append("\n");
   
    }  
}

try {
    java.nio.file.Files.write(java.nio.file.Paths.get("decay_data.csv"), csvContent.toString().getBytes());
    System.out.println(">>>success! decay_data.csv");
   } catch (java.io.IOException e) {
   System.out.println("error has occurred " + e.getMessage());
}
   
System.out.println("-----------------------------------------------------------------");
System.out.printf("The projected life of orbit is : %.2f days\n" , time / 86400);
}

public static double findDensity(double h) {
    double altKm = h/1000.0;
    if (altKm > 150) {
        return 2.789e-10 * Math.exp(-(altKm - 200)/ 37.5);
    }
    else {
        return 1.225 * Math.exp(-altKm / 8.5);
    }
}
}

