namespace SlackBot.Models {

  /// <summary>
  ///
  /// </summary>
  public class BlockKitMessage {

    /// <summary>
    ///
    /// </summary>
    public List<object> blocks { get; set; } = new List<object>();
  }

  /// <summary>
  ///
  /// </summary>
  public class PlainTextDescriptor {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "plain_text";

    /// <summary>
    ///
    /// </summary>
    public string? text { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class HeaderBlock {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "header";

    /// <summary>
    ///
    /// </summary>
    public PlainTextDescriptor text { get; set; } = new PlainTextDescriptor();
  }

  /// <summary>
  ///
  /// </summary>
  public class DividerBlock {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "divider";
  }

  /// <summary>
  ///
  /// </summary>
  public class MarkdownTextDescriptor {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "mrkdwn";

    /// <summary>
    ///
    /// </summary>
    public string? text { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class SectionBlock {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "section";

    /// <summary>
    ///
    /// </summary>
    public MarkdownTextDescriptor text { get; set; } = new MarkdownTextDescriptor();

    /// <summary>
    ///
    /// </summary>
    public object accessory { get; set; } = new object();
  }

  /// <summary>
  ///
  /// </summary>
  public class StaticSelectAccessory {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "static_select";

    /// <summary>
    ///
    /// </summary>
    public SelectPlaceHolder? placeholder { get; set; }

    /// <summary>
    ///
    /// </summary>
    public List<SelectOption> options { get; set; } = new List<SelectOption>();

    /// <summary>
    ///
    /// </summary>
    public string? action_id { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class SelectOption {

    /// <summary>
    ///
    /// </summary>
    public string? value { get; set; }

    /// <summary>
    ///
    /// </summary>
    public SelectOptionText text { get; set; } = new SelectOptionText();
  }

  /// <summary>
  ///
  /// </summary>
  public class SelectOptionText {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "plain_text";

    /// <summary>
    ///
    /// </summary>
    public string? text { get; set; }

    /// <summary>
    ///
    /// </summary>
    public bool emoji { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class SelectPlaceHolder {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "plain_text";

    /// <summary>
    ///
    /// </summary>
    public string? text { get; set; }

    /// <summary>
    ///
    /// </summary>
    public bool emoji { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class OverflowAccessory {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "overflow";

    /// <summary>
    ///
    /// </summary>
    public List<OverflowOption> options { get; set; } = new List<OverflowOption>();

    /// <summary>
    ///
    /// </summary>
    public string? action_id { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class OverflowOption {

    /// <summary>
    ///
    /// </summary>
    public string? value { get; set; }

    /// <summary>
    ///
    /// </summary>
    public OverflowText text { get; set; } = new OverflowText();
  }

  /// <summary>
  ///
  /// </summary>
  public class OverflowText {

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; } = "plain_text";

    /// <summary>
    ///
    /// </summary>
    public string? text { get; set; }

    /// <summary>
    ///
    /// </summary>
    public bool emoji { get; set; }
  }
}
