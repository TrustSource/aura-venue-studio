(* Control logic of the installation "Hotel Lobby". Written by the integrator. *)
PROGRAM Main
VAR
    FireAlarm  : BOOL;        (* from the fire alarm panel via Modbus TCP *)
    Panel      : OpenField.ModbusTcp.Client;
    Paging     : Aurelia.Paging.Announcement;
END_VAR

Panel(Host := '10.20.0.15', Port := 502);
FireAlarm := Panel.ReadCoil(Address := 100);

IF FireAlarm THEN
    Paging(Zone := 'Zone 3', Message := 'evacuation', Priority := 1);
END_IF
END_PROGRAM
