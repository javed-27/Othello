interface HeaderProps {
  onRestart: () => void;
}

function Header({ onRestart }: HeaderProps) {
  return (
    <div className="header">
      <h1>Othello</h1>
      <button className="restart-button" onClick={onRestart}>
        New Game
      </button>
    </div>
  );
}

export default Header;
