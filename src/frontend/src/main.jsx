import React from 'react';
import { createRoot } from 'react-dom/client';
import * as monaco from 'monaco-editor';
import { amplifiersByZone, onEdit } from './zones.js';

function App() {
  const editorRef = React.useRef(null);

  React.useEffect(() => {
    const editor = monaco.editor.create(editorRef.current, {
      value: 'IF FireAlarm THEN Page(Zone3, "evacuation") END_IF',
      language: 'plaintext',
    });
    const save = onEdit(() => window.chrome?.webview?.postMessage({ logic: editor.getValue() }));
    editor.onDidChangeModelContent(save);
    return () => editor.dispose();
  }, []);

  const zones = amplifiersByZone([
    { name: 'AMP-01', zone: 'Lobby' },
    { name: 'AMP-02', zone: 'Lobby' },
    { name: 'AMP-03', zone: 'Zone 3' },
  ]);

  return (
    <main>
      <h1>Venue Studio</h1>
      <ul>
        {Object.entries(zones).map(([zone, amps]) => (
          <li key={zone}>{zone}: {amps.map((a) => a.name).join(', ')}</li>
        ))}
      </ul>
      <div ref={editorRef} style={{ height: 400 }} />
    </main>
  );
}

createRoot(document.getElementById('root')).render(<App />);
